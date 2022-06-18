using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WBH.Livescoring.IoC;

internal static class AssemblyScanner
{
    #region Methods

    /// <summary>
    ///     Liest alle Assemblies aus dem aktuellen Thread aus und lädt fehlende nach
    /// </summary>
    /// <returns>Liste der Assemblies</returns>
    public static IEnumerable<Assembly> GetAssemblies()
    {
        // Geladene Assemblies auslesen
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

        // Pfad für die geladenen Assemblies bestimmen
        var loadedPaths = assemblies.Select(a => a.Location).ToArray();

        // Fehlende Assemblies bestimmen
        var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "WBH.*.dll",
            SearchOption.AllDirectories);
        var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase))
            .ToList();

        // Fehlende Assemblies laden
        toLoad.ForEach(path => assemblies.Add(GetDotNetAssembly(path)));

        // Assemblies ausgeben
        return assemblies;
    }

    private static Assembly GetDotNetAssembly(string assemblyFile)
    {
        try
        {
            return Assembly.LoadFrom(assemblyFile);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    ///     Lädt alle Module aus allen Assemblies in die Service Collection
    /// </summary>
    /// <param name="collection">Service Collection</param>
    /// <returns>Service Collection</returns>
    public static IServiceCollection LoadModules(IServiceCollection collection)
    {
        // Interface Typ bestimmen
        var moduleType = typeof(IModule);

        // Modul Typen auslesen
        var moduleTypes = GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && !t.IsInterface && moduleType.IsAssignableFrom(t))
            .ToList();

        // Module laden
        foreach (var type in moduleTypes) collection.AddSingleton(moduleType, type);

        // Service Collection ausgeben
        return collection;
    }

    #endregion
}