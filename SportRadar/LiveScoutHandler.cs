using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Sportradar.LiveData.Sdk.FeedProviders.Common.Events;
using Sportradar.LiveData.Sdk.FeedProviders.LiveScout.Events;

namespace WBH.Livescoring.SportRadar;

// ReSharper disable PossibleMultipleEnumeration
internal sealed class LiveScoutHandler
{
    #region Constructors

    public LiveScoutHandler(IEnumerable<Func<object, ILogger>> loggingFactories, Func<Type, IEnumerable<object>> servicesFactory)
    {
        _servicesFactory = servicesFactory;
        if (loggingFactories.Any())
            _logger = loggingFactories
                .Take(1)
                .Select(f => f(this))
                .FirstOrDefault();
    }

    #endregion

    #region Fields

    private readonly Func<Type, IEnumerable<object>> _servicesFactory;
    private readonly ILogger _logger;

    #endregion

    #region Methods

    private IEnumerable<TEventHandler> GetEventHandlers<TEventHandler>()
    {
        return _servicesFactory(typeof(ILiveScoutEventHandler))
            .Union(_servicesFactory(typeof(TEventHandler)))
            .OfType<TEventHandler>()
            .Distinct()
            .ToList();
    }

    public void OnOpened(object sender, ConnectionChangeEventArgs e)
    {
        _logger.LogInformation("We opened a LiveScout for you: {0}", e.LocalTimestamp);
    }

    public void ClosedHandler(object sender, ConnectionChangeEventArgs e)
    {
        // Handler abrufen
        var eventHandlers = GetEventHandlers<ILiveScoutClosedHandler>();
        
        // Alle Handler informieren
        foreach (var handler in eventHandlers) handler.Handle(e.LocalTimestamp);
    }

    public void MatchBookingReplyHandler(object sender, MatchBookingReplyEventArgs e)
    {
        var mb = e.MatchBooking;
        var matchBookingReply = new MatchBookingReply
        {
            MatchId = mb.MatchId,
            Message = mb.Message,
            Result = (BookMatchResult)(int)mb.Result
        };
        
        // Handler abrufen
        var eventHandlers = GetEventHandlers<ILiveScoutMatchBookingReplyHandler>();
        
        // Alle Handler informieren
        foreach (var handler in eventHandlers) handler.Handle(matchBookingReply);
    }

    public void MatchDataHandler(object sender, MatchDataEventArgs e)
    {
        var md = e.MatchData;
        var data = new MatchData
        {
            MatchId = md.MatchId,
            MatchTime = md.MatchTime
        };

        // Handler abrufen
        var eventHandlers = GetEventHandlers<ILiveScoutMatchDataHandler>();
        
        // Alle Handler informieren
        foreach (var handler in eventHandlers) handler.Handle(data);
    }

    public void MatchListHandler(object sender, MatchListEventArgs e)
    {
        var result = new List<MatchListItem>();
        foreach (var item in e.MatchList)
        {
            var pushItem = new MatchListItem
            {
                MatchId = item.MatchHeader.MatchId,
                T1Name = item.MatchHeader.Team1.Name.International,
                T2Name = item.MatchHeader.Team2.Name.International,
                CourtName = item.Court.Name,
                TournamentName = item.Tournament.Name.International,
                MatchStatus = (ScoutMatchStatus)(int)item.MatchStatus
            };

            result.Add(pushItem);
        }

        _logger.LogInformation("These are the available matches: {0}", result);
    }

    public void MatchStopHandler(object sender, MatchStopEventArgs e)
    {
        _logger.LogInformation("The following match was stopped: {0}", e.MatchId);
    }

    public void MatchUpdateHandler(object sender, MatchUpdateEventArgs e)
    {
        var mu = e.MatchUpdate;
        var update = new MatchUpdate
        {
            CourtName = mu.Court.Name,
            MatchId = mu.MatchHeader.MatchId,
            Serve = (Team)(int)mu.Serve,
            T1Name = mu.MatchHeader.Team1.Name.International,
            T2Name = mu.MatchHeader.Team2.Name.International,
            TournamentName = mu.Tournament.Name.International
        };

        // Handler abrufen
        var eventHandlers = GetEventHandlers<ILiveScoutMatchUpdateHandler>();
        
        // Alle Handler informieren
        foreach (var handler in eventHandlers) handler.Handle(update);
    }

    public void MatchUpdateDeltaHandler(object sender, MatchUpdateEventArgs e)
    {
        var mu = e.MatchUpdate;
        var update = new MatchUpdateDelta
        {
            CourtName = mu.Court?.Name,
            MatchId = mu.MatchHeader.MatchId,
            Serve = (Team?)(int?)mu.Serve,
            T1Name = mu.MatchHeader?.Team1?.Name?.International,
            T2Name = mu.MatchHeader?.Team2?.Name?.International,
            TournamentName = mu.Tournament?.Name?.International
        };

        // Handler abrufen
        var eventHandlers = GetEventHandlers<ILiveScoutMatchUpdateDeltaHandler>();
        
        // Alle Handler informieren
        foreach (var handler in eventHandlers) handler.Handle(update);
    }

    public void MatchUpdateDeltaUpdateHandler(object sender, MatchUpdateEventArgs e)
    {
        var mu = e.MatchUpdate;
        var update = new MatchUpdateDeltaUpdate
        {
            MatchId = mu.MatchHeader.MatchId,
            Scores = mu.Scores?.Select(s => new Score
            {
                Team1 = s.Team1,
                Team2 = s.Team2,
                Type = s.Type
            }),
            Serve = (Team)(int)mu.Serve
        };

        // Handler abrufen
        var eventHandlers = GetEventHandlers<ILiveScoutMatchUpdateDeltaUpdateHandler>();
        
        // Alle Handler informieren
        foreach (var handler in eventHandlers) handler.Handle(update);
    }

    public void MatchUpdateFullHandler(object sender, MatchUpdateEventArgs e)
    {
        var mu = e.MatchUpdate;
        var update = new MatchUpdate
        {
            MatchId = mu.MatchHeader.MatchId,
            Scores = mu.Scores?.Select(s => new Score
            {
                Team1 = s.Team1,
                Team2 = s.Team2,
                Type = s.Type
            }),
            Serve = (Team)(int)mu.Serve,
            CourtName = mu.Court.Name,
            TournamentName = mu.Tournament.Name.International,
            T1Name = mu.MatchHeader.Team1.Name.International,
            T2Name = mu.MatchHeader.Team2.Name.International
        };

        // Handler abrufen
        var eventHandlers = GetEventHandlers<ILiveScoutMatchUpdateFullHandler>();
        
        // Alle Handler informieren
        foreach (var handler in eventHandlers) handler.Handle(update);
    }

    public void FeedErrorHandler(object sender, FeedErrorEventArgs e)
    {
        _logger?.LogError("Error: {ErrorMessage} {Cause} at {Timestamp}", e.ErrorMessage, e.Cause, e.LocalTimestamp);
    }

    #endregion
}