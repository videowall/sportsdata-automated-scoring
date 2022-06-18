using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace WBH.Livescoring.IoC;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions
{
    #region Methods

    /// <summary>
    ///     Installiert die Module aus allen Assemblies des aktuellen Threads
    /// </summary>
    /// <param name="collection">Service Collection</param>
    /// <returns>Service Collection</returns>
    public static IServiceCollection InstallModules(this IServiceCollection collection)
    {
        // Module laden
        AssemblyScanner.LoadModules(collection);

        // Service Provider initialisieren
        var serviceProvider = collection.BuildServiceProvider();

        // Alle Module abrufen
        foreach (var module in serviceProvider.GetServices<IModule>()) module.RegisterServices(collection);

        // Collection ausgeben
        return collection;
    }

    public static IServiceCollection RegisterTransientAllTypesOf<TType, TServiceNamespace>(this IServiceCollection collection)
    {
        // Typ und Assembly bestimmen
        var type = typeof(TType);
        var namespaceType = typeof(TServiceNamespace);
        var assembly = namespaceType.Assembly;

        // Alle Implementierungen aus der Assembly auslesen
        var types = assembly.GetTypes()
            .Where(t => t.Namespace != null && !t.IsAbstract && !t.IsInterface && type.IsAssignableFrom(t) && t.Namespace.StartsWith(namespaceType.Namespace ?? string.Empty))
            .ToList();

        // Typ registrieren
        foreach (var implementationType in types)
        {
            collection.AddTransient(type, implementationType);
        }

        // Collection ausgeben
        return collection;
    }

    #endregion
}