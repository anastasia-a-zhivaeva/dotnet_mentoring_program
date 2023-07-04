using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;

namespace BrainstormSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(new JsonFormatter(), "C:/Temp/Log/BrainstormSessions", shared: true)
                .CreateLogger();

            try
            {
                Log.Information("Starting web application...");

                var builder = CreateHostBuilder(args);

                builder.UseSerilog();

                var app = builder.Build();
                    
                app.Run();

                Log.Information("App is running successfully");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
