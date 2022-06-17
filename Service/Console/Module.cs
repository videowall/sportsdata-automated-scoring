using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WBH.Livescoring.IoC;
using WBH.Livescoring.Service.Console.IoC;
using WBH.Livescoring.Service.IoC;

namespace WBH.Livescoring.Service.Console
{
    internal class Module : IModule
    {
        #region IModule

        public void RegisterServices(IServiceCollection container)
        {
            // Konfiguration registrieren
            var config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            container.AddSingleton<IConfiguration>(config);

            // Logger laden und verfügbar machen
            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(config)
                .CreateLogger();
            Log.Logger = logger;
            container.AddTransient<ILogger>(_ => logger);

            // Test Runner registrieren
            container.AddTransient<IRunner<Runner.TestOption>, Runner.TestRunner>();
        }

        #endregion
    }
}
