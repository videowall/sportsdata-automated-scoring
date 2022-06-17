using Microsoft.Extensions.DependencyInjection;
using Sportradar.LiveData.Sdk.Services;
using WBH.Livescoring.IoC;
using WBH.Livescoring.Service.IoC;
using WBH.Livescoring.Service.Logic.LiveScout;

namespace WBH.Livescoring.Service.Logic;

public sealed class Module : IModule
{
    #region IModule

    public void RegisterServices(IServiceCollection container)
    {
        // Bookmaker SDK registrieren
        container.AddSingleton(s =>
        {
            // Instanz abrufen
            var sdk = Sdk.Instance;

            // Instanz initialisieren
            sdk.Initialize();

            // Instanz starten
            sdk.Start();

            // Instanz ausgeben
            return sdk;
        });

        // Bookmaker LiveScout registrieren
        container.AddSingleton(s => s.GetRequiredService<Sdk>().LiveScout);

        // Start Tasks registrieren
        container.AddTransient<IStartupTask, RegisterLiveScoutEventsTask>();

        // Shutdown Tasks registrieren
        container.AddTransient<IShutdownTask, StopLiveScoutTask>();

        // LiveScout Eventhandler registrieren
        container.AddTransient<LiveScoutHandler>();
    }

    #endregion
}