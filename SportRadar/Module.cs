using System;
using Microsoft.Extensions.DependencyInjection;
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

            // Instanz starten
            sdk.Start();

            // Instanz ausgeben
            return sdk;
        });

        // Bookmaker LiveScout registrieren
        container.AddSingleton(s =>
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
            liveScout.OnLineups += handler.LineupsHandler;
            liveScout.OnMatchBookingReply += handler.MatchBookingReplyHandler;
            liveScout.OnMatchData += handler.MatchDataHandler;
            liveScout.OnMatchList += handler.MatchListHandler;
            liveScout.OnMatchListUpdate += handler.MatchListUpdateHandler;
            liveScout.OnMatchStop += handler.MatchStopHandler;
            liveScout.OnMatchUpdate += handler.MatchUpdateHandler;
            liveScout.OnMatchUpdateDelta += handler.MatchUpdateDeltaHandler;
            liveScout.OnMatchUpdateDeltaUpdate += handler.MatchUpdateDeltaUpdateHandler;
            liveScout.OnMatchUpdateFull += handler.MatchUpdateFullHandler;
            liveScout.OnOddsSuggestion += handler.OddsSuggestionHandler;
            liveScout.OnScoutInfo += handler.ScoutInfoHandler;
            liveScout.OnFeedError += handler.FeedErrorHandler;

            // LiveScout Shutdown registrieren
            AppDomain.CurrentDomain.ProcessExit += (_, _) =>
            {
                liveScout.Stop();
                sdk.Stop();
            };

            // LiveScout registrieren
            return liveScout;
        });
    }

    #endregion
}