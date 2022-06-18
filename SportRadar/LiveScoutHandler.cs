using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Sportradar.LiveData.Sdk.FeedProviders.Common.Enums;
using Sportradar.LiveData.Sdk.FeedProviders.Common.Events;
using Sportradar.LiveData.Sdk.FeedProviders.LiveScout.Entities;
using Sportradar.LiveData.Sdk.FeedProviders.LiveScout.Events;
using Sportradar.LiveData.Sdk.Providers.Protocols.LiveScout.Server;

namespace WBH.Livescoring.SportRadar;

// ReSharper disable PossibleMultipleEnumeration
internal sealed class LiveScoutHandler
{

    #region Fields

    private readonly IEnumerable<ILiveScoutEventHandler> _eventHandlers;
    private readonly ILogger _logger;

    #endregion

    #region Constructors

    public LiveScoutHandler(
        IEnumerable<Func<object, ILogger>> loggingFactories,
        IEnumerable<ILiveScoutEventHandler> eventHandlers,
        IEnumerable<ILiveScoutOnOpenedEventHandler> onOpenedEventHandlers
    )
    {
        if (loggingFactories.Any())
        { 
            _logger = loggingFactories
                .Take(1)
                .Select(f => f(this))
                .FirstOrDefault();
        }

        if (eventHandlers?.Any() == true)
        {
            _eventHandlers = eventHandlers;
        }
        else
        {
            _eventHandlers = new List<ILiveScoutEventHandler>();
            ((List<ILiveScoutEventHandler>) _eventHandlers).AddRange(onOpenedEventHandlers);
        }
    }

    #endregion

    #region Methods

    public void OnOpened(object sender, ConnectionChangeEventArgs e)
    {
        // Alle Handler informieren
        foreach (var handler in _eventHandlers.OfType<ILiveScoutOnOpenedEventHandler>())
        {
            handler.Handle(e.LocalTimestamp);
        }
    }

    public void ClosedHandler(object sender, ConnectionChangeEventArgs e)
    {
        foreach (var handler in _eventHandlers.OfType<ILiveScoutClosedHandler>())
        {
            handler.Handle(e.LocalTimestamp);
        }
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
        foreach (var handler in _eventHandlers.OfType<ILiveScoutMatchBookingReplyHandler>())
        {
            handler.Handle(matchBookingReply);
        }
    }

    public void MatchDataHandler(object sender, MatchDataEventArgs e)
    {
        var md = e.MatchData;
        var data = new MatchData
        {
            MatchId = md.MatchId,
            MatchTime = md.MatchTime
        };
        
        foreach (var handler in _eventHandlers.OfType<ILiveScoutMatchDataHandler>())
        {
            handler.Handle(data);
        }
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

        foreach (var handler in _eventHandlers.OfType<ILiveScoutMatchListHandler>())
        {
            handler.Handle(result);
        }
    }

    public void MatchStopHandler(object sender, MatchStopEventArgs e)
    {
        foreach (var handler in _eventHandlers.OfType<ILiveScoutMatchStopHandler>())
        {
            handler.Handle(e.MatchId, e.Reason);
        }
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

        foreach (var handler in _eventHandlers.OfType<ILiveScoutMatchUpdateHandler>())
        {
            handler.Handle(update);
        }
    }

    public void MatchUpdateDeltaHandler(object sender, MatchUpdateEventArgs e)
    {
        var mu = e.MatchUpdate;
        var update = new MatchUpdateDelta
        {
            CourtName = mu.Court?.Name,
            MatchId = mu.MatchHeader?.MatchId,
            Serve = (Team?)(int?)mu.Serve,
            T1Name = mu.MatchHeader?.Team1?.Name?.International,
            T2Name = mu.MatchHeader?.Team2?.Name?.International,
            TournamentName = mu.Tournament?.Name?.International
        };

        foreach (var handler in _eventHandlers.OfType<ILiveScoutMatchUpdateDeltaHandler>())
        {
            handler.Handle(update);
        }
    }

    public void MatchUpdateDeltaUpdateHandler(object sender, MatchUpdateEventArgs e)
    {
        var mu = e.MatchUpdate;
        var update = new MatchUpdateDeltaUpdate
        {
            MatchId = mu.MatchHeader.MatchId,
            Scores = mu.Scores?.Select(s=>new Score
            {
                Team1 = s.Team1,
                Team2 = s.Team2,
                Type = s.Type
            }),
            Serve = (Team)(int)mu.Serve,
        };

        foreach (var handler in _eventHandlers.OfType<ILiveScoutMatchUpdateDeltaUpdateHandler>())
        {
            handler.Handle(update);
        }
    }

    public void MatchUpdateFullHandler(object sender, MatchUpdateEventArgs e)
    {
        var mu = e.MatchUpdate;
        var update = new MatchUpdate
        {
            MatchId = mu.MatchHeader.MatchId,
            Scores = mu.Scores?.Select(s=>new Score
            {
                Team1 = s.Team1,
                Team2 = s.Team2,
                Type = s.Type
            }),
            Serve = (Team)(int)mu.Serve,
            CourtName = mu.Court.Name,
            TournamentName = mu.Tournament.Name.International,
            T1Name = mu.MatchHeader.Team1.Name.International,
            T2Name = mu.MatchHeader.Team2.Name.International,
        };

        foreach (var handler in _eventHandlers.OfType<ILiveScoutMatchUpdateFullHandler>())
        {
            handler.Handle(update);
        }
    }

    public void FeedErrorHandler(object sender, FeedErrorEventArgs e)
    {
        _logger?.LogError("Error: {ErrorMessage} {Cause} at {Timestamp}", e.ErrorMessage, e.Cause, e.LocalTimestamp);
    }

    #endregion
}