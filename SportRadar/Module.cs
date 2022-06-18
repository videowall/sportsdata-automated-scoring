using System;
using Microsoft.Extensions.DependencyInjection;
using Sportradar.LiveData.Sdk.FeedProviders.LiveScout.Interfaces;
using Sportradar.LiveData.Sdk.Services;
using WBH.Livescoring.IoC;

namespace WBH.Livescoring.SportRadar;

public sealed class Module : IModule
{
    #region IModule

    public void RegisterServices(IServiceCollection container)
    {
        // LiveScout Eventhandler registrieren
        container.AddTransient<LiveScoutHandler>();

        // Bookmaker SDK registrieren
        container.AddSingleton(_ =>
        {
            // Instanz abrufen
            var sdk = Sdk.Instance;

            // Instanz initialisieren
            sdk.Initialize();

            // Instanz registrieren
            return sdk;
        });

        // Bookmaker LiveScout registrieren
        container.AddSingleton<Func<ILiveScout>>(s => () =>
        {
            // Handler abrufen
            var handler = s.GetRequiredService<LiveScoutHandler>();

            // SDK abrufen
            var sdk = s.GetRequiredService<Sdk>();

            // LiveScout abrufen
            var liveScout = sdk.LiveScout;

            // Events registrieren
            liveScout.OnOpened += handler.OnOpened;
            liveScout.OnClosed += handler.ClosedHandler;
            liveScout.OnMatchBookingReply += handler.MatchBookingReplyHandler;
            liveScout.OnMatchData += handler.MatchDataHandler;
            liveScout.OnMatchList += handler.MatchListHandler;
            liveScout.OnMatchStop += handler.MatchStopHandler;
            liveScout.OnMatchUpdate += handler.MatchUpdateHandler;
            liveScout.OnMatchUpdateDelta += handler.MatchUpdateDeltaHandler;
            liveScout.OnMatchUpdateDeltaUpdate += handler.MatchUpdateDeltaUpdateHandler;
            liveScout.OnMatchUpdateFull += handler.MatchUpdateFullHandler;
            liveScout.OnFeedError += handler.FeedErrorHandler;

            // LiveScout registrieren
            return liveScout;
        });

        // LiveScout Service registrieren
        container.AddTransient<ILiveScoutService, LiveScoutService>();
    }

    #endregion
}