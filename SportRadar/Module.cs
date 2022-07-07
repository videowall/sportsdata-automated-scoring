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
            liveScout.OnOpened += async (sender, e) => await handler.OnOpened(sender, e);
            liveScout.OnClosed += async (sender, e) => await handler.ClosedHandler(sender, e);
            liveScout.OnMatchBookingReply += async (sender, e) => await handler.MatchBookingReplyHandler(sender, e);
            liveScout.OnMatchData += async (sender, e) => await handler.MatchDataHandler(sender, e);
            liveScout.OnMatchList += async (sender, e) => await handler.MatchListHandler(sender, e);
            liveScout.OnMatchStop += async (sender, e) => await handler.MatchStopHandler(sender, e);
            liveScout.OnMatchUpdate += async (sender, e) => await handler.MatchUpdateHandler(sender, e);
            liveScout.OnMatchUpdateDelta += async (sender, e) => await handler.MatchUpdateDeltaHandler(sender, e);
            liveScout.OnMatchUpdateDeltaUpdate += async (sender, e) => await handler.MatchUpdateDeltaUpdateHandler(sender, e);
            liveScout.OnMatchUpdateFull += async (sender, e) => await handler.MatchUpdateFullHandler(sender, e);
            liveScout.OnFeedError += async (sender, e) => await handler.FeedErrorHandler(sender, e);

            // LiveScout registrieren
            return liveScout;
        });

        // LiveScout Service registrieren
        container.AddTransient<ILiveScoutService, LiveScoutService>();
    }

    #endregion
}