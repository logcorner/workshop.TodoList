using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TodoList.WebApi.Exceptions;

namespace TodoList.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Log.Logger);
            services.AddApplicationInsightsTelemetry(Configuration["ApplicationInsights:InstrumentationKey"]);

            //  services.AddSingleton<IDatabase<Todo>, Database>();

            services.AddToDoServices();

            services.AddCustomApiVersioning();

            services.AddControllers(x =>
            {
                x.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
                x.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                x.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                x.Filters.Add(new ProducesDefaultResponseTypeAttribute());
            });

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/healthcheck");
            });
        }
    }
}