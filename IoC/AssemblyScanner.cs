using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WBH.Livescoring.IoC
{
    internal static class AssemblyScanner
    {
        #region Methods

        /// <summary>
        /// Liest alle Assemblies aus dem aktuellen Thread aus
        /// </summary>
        /// <returns>Liste der Assemblies</returns>
        public static IEnumerable<Assembly> GetAssemblies() => System.AppDomain.CurrentDomain.GetAssemblies();

        /// <summary>
        /// Lädt alle Module aus allen Assemblies in die Service Collection
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
            foreach (var type in moduleTypes)
            {
                collection.AddSingleton(moduleType, type);
            }

            // Service Collection ausgeben
            return collection;
        }

        #endregion
    }
}