using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using WBH.Livescoring.IoC;
using WBH.Livescoring.Service.IoC;

namespace WBH.Livescoring.Service.Console.IoC;

internal class ConsoleBootstrapper
{
    #region Fields
    
    private readonly IServiceCollection services;

    #endregion

    #region Properties

    public IServiceProvider ServiceProvider { get; private set; }

    #endregion

    #region Constructors

    public ConsoleBootstrapper()
    {
        services = new ServiceCollection();
    }

    #endregion

    #region Methods

    public bool Run(IRunnerVerb verb)
    {
        // Module installieren
        services.InstallModules();

        // Services bauen
        ServiceProvider = services.BuildServiceProvider();

        // Rückgabewert
        var returnValue = true;

        try
        {
            // Start Methoden auslesen
            var serviceType = typeof(IStartupTask);
            var services = ServiceProvider
                .GetServices(serviceType)
                .ToList();

            // Start Methoden ausführen
            foreach (var instance in services)
            {
                serviceType.GetMethod(nameof(IStartupTask.Start))?.Invoke(instance, Array.Empty<object>());
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine("ERROR: " + e.Message);
            return false;
        }

        try
        {
            // Services auslesen
            var serviceType = typeof(IRunner<>).MakeGenericType(verb.GetType());
            var services = ServiceProvider
                .GetServices(serviceType)
                .ToList();

            // Services ausführen
            foreach (var instance in services)
            {
                var result = serviceType.GetMethod(nameof(IRunner<IRunnerVerb>.Run))?.Invoke(instance, new object[] { verb });
                returnValue = returnValue && result != null && Convert.ToBoolean(result);
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine("ERROR: " + e.Message);
            return false;
        }

        // Ergebnis ausgeben
        return returnValue;
    }

    public void Stop()
    {
        try
        {
            // Aufgaben zum Beenden abrufen
            var serviceType = typeof(IShutdownTask);
            var services = ServiceProvider
                .GetServices(serviceType)
                .ToList();

            // Aufgaben ausführen
            foreach (var instance in services)
            {
                serviceType.GetMethod(nameof(IShutdownTask.Shutdown))?.Invoke(instance, Array.Empty<object>());
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine("ERROR: " + e.Message);
            return;
        }
    }

    #endregion
}