using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TodoList.Application;
using TodoList.Domain;
using TodoList.Infrastructure;

namespace TodoList.WebApi
{
    public static class ServicesConfiguration
    {
        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(options => { options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; }
                ).AddJwtBearer(options => configuration.Bind("AzureAd", options));
        }

        public static void AddCustomSwaggerGen(this IServiceCollection services, IConfiguration configuration, bool useIdentity = false)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Todo List - Web ApiHTTP API",
                    Version = "v1",
                    Description = "The Web ApiService HTTP API",
                    Contact = new OpenApiContact
                    {
                        Email = "jean.dupont@gmail.com",
                        Name = "jean dupont",
                        Url = new Uri("https://www.webapihost.com/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                if (File.Exists(xmlCommentsFullPath))
                {
                    options.IncludeXmlComments(xmlCommentsFullPath);
                }

                if (useIdentity)
                {
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,

                        Flows = new OpenApiOAuthFlows
                        {
                            AuthorizationCode = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl =
                                    new Uri(
                                        $"https://login.microsoftonline.com/{configuration["AzureAd:TenantId"]}/oauth2/v2.0/authorize"),
                                TokenUrl = new Uri(
                                    $"https://login.microsoftonline.com/{configuration["AzureAd:TenantId"]}/oauth2/v2.0/token"),
                                Scopes = new Dictionary<string, string>
                                {
                                    {
                                        $"https://{configuration["AzureAd:Domain"]}/api/standard-web-api/ToDoListItem.Create",
                                        "Create a ToDoList"
                                    },
                                    {
                                        $"https://{configuration["AzureAd:Domain"]}/api/standard-web-api/ToDoListItem.Delete",
                                        "Delete a ToDoList"
                                    },
                                    {
                                        $"https://{configuration["AzureAd:Domain"]}/api/standard-web-api/ToDoListItem.Edit",
                                        "Edit a ToDoList"
                                    },
                                    {
                                        $"https://{configuration["AzureAd:Domain"]}/api/standard-web-api/ToDoListItem.Read",
                                        "Read a ToDoList"
                                    }
                                }
                            }
                        }
                    });

                    options.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "oauth2"
                                    }
                                },
                                new[]
                                {
                                    $"https://{configuration["AzureAd:Domain"]}/api/standard-web-api/ToDoListItem.Create",
                                    $"https://{configuration["AzureAd:Domain"]}/api/standard-web-api/ToDoListItem.Delete",
                                    $"https://{configuration["AzureAd:Domain"]}/api/standard-web-api/ToDoListItem.Edit",
                                    $"https://{configuration["AzureAd:Domain"]}/api/standard-web-api/ToDoListItem.Read"
                                }
                            }
                        });
                }
            });
        }

        public static void AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            var corsPolicy = configuration["CorsPolicy:AllowedOrigins"]?.Split(";");
            if (corsPolicy == null || !corsPolicy.Any())
            {
                throw new Exception("cannot add AddCustomCors, please check your appsettings ");
            }
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.WithOrigins(corsPolicy)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        public static void AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
        }

        public static void AddToDoServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoUseCase, TodoUseCase>();

            //services.AddSingleton<IDatabase<Todo>, Database>();
            services.AddScoped<IRepository<Todo>, TodoRepository>();
        }
    }
}