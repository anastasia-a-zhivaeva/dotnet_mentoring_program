using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Email;

namespace BrainstormSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var loggerConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Logger");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(new JsonFormatter(), loggerConfig["LogFilePath"], shared: true)
                .WriteTo.Email(
                    new EmailConnectionInfo {
                        FromEmail = loggerConfig["FromEmail"],
                        ToEmail = loggerConfig["ToEmail"],
                        MailServer = "smtp.gmail.com",
                        NetworkCredentials = new NetworkCredential(loggerConfig["Login"], loggerConfig["Password"]),
                        EnableSsl = true,
                        Port = 465,
                        EmailSubject = loggerConfig["EmailSubject"]
                    },
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information, // Usually, minimum level is Error
                    batchPostingLimit: loggerConfig.GetValue<int>("BatchPostingLimit"),
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm} [{Level}] {Message}{NewLine}{Exception}")
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
