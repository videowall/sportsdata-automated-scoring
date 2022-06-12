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
        var provider = services.BuildServiceProvider();

        // Rückgabewert
        var returnValue = true;

        try
        {
            // Services auslesen
            var serviceType = typeof(IRunner<>).MakeGenericType(verb.GetType());
            var services = provider
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

    #endregion
}