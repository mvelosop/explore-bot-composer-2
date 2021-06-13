using Destructurama;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Linq;

namespace ExplorationBot.Configuration
{
    public static class LoggingConfigurationExtensions
    {
        private static readonly string[] devEnvironments = new[] { "Development", "Testing" };

        public static LoggerConfiguration ConfigureSerilogDefaults(this LoggerConfiguration loggerConfiguration, string seqUrl, string appName, string hostName, IHostEnvironment hostEnvironment = null)
        {
            loggerConfiguration
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", appName)
                .Enrich.WithProperty("HostName", hostName)
                .Destructure.JsonNetTypes() // Enable JObject destruturing
                .WriteTo.Console()
                .WriteTo.File( // Write to standard Azure AppService-monitored log location
                    $@"D:\home\LogFiles\{appName}-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1));

            var environment = hostEnvironment is null
                ? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                : hostEnvironment.EnvironmentName;

            // Add Seq for development or if configured
            if (string.IsNullOrWhiteSpace(seqUrl))
            {
                if (devEnvironments.Contains(environment, StringComparer.OrdinalIgnoreCase))
                {
                    loggerConfiguration.WriteTo.Seq("http://localhost:5341");
                }
            }
            else
            {
                loggerConfiguration.WriteTo.Seq(seqUrl);
            }

            return loggerConfiguration;
        }
    }
}
