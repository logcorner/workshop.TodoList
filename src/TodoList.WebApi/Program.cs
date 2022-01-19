using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace TodoList.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
                Log.Information("Starting web host");
                Console.WriteLine("running");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Host terminated unexpectedly :{ex.Message}");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureAppConfiguration((_, config) =>
                {
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    Console.WriteLine($"*** ApiStandard.WebApi is running and listening on https://localhost:5001;http://localhost:5000 ***");
                    Console.WriteLine($"*** Please visit  https://localhost:5001/swagger/index.html in a web browser ***");
                    Console.WriteLine($"*** Press CTRL+C to exit ***");
                    //
                });
    }
}