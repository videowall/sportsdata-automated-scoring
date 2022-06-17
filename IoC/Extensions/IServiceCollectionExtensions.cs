using Microsoft.Extensions.DependencyInjection;

namespace WBH.Livescoring.IoC
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        #region Methods

        /// <summary>
        /// Installiert die Module aus allen Assemblies des aktuellen Threads
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
            foreach (var module in serviceProvider.GetServices<IModule>())
            {
                module.RegisterServices(collection);
            }
            
            // Collection ausgeben
            return collection;
        }

        #endregion
    }
}