using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sportradar.LiveData.Sdk.FeedProviders.Common.Events;
using Sportradar.LiveData.Sdk.FeedProviders.LiveScout.Events;

namespace WBH.Livescoring.SportRadar;

// ReSharper disable PossibleMultipleEnumeration
internal sealed class LiveScoutHandler
{
    #region Constructors

    public LiveScoutHandler(IEnumerable<Func<object, ILogger>> loggingFactories, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        if (loggingFactories.Any())
            _logger = loggingFactories
                .Take(1)
                .Select(f => f(this))
                .FirstOrDefault();
    }

    #endregion

    #region Fields

    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    #endregion

    #region Methods

    private IEnumerable<TEventHandler> GetEventHandlers<TEventHandler>() where TEventHandler: ILiveScoutEventHandler
    {
        return _serviceProvider.GetServices<ILiveScoutEventHandler>()
            .Union(_serviceProvider.GetServices<TEventHandler>().OfType<ILiveScoutEventHandler>())
            .OfType<TEventHandler>()
            .Distinct()
            .ToList();
    }

    public Task OnOpened(object sender, ConnectionChangeEventArgs e)
    {
        _logger.LogInformation("We opened a LiveScout for you: {LocalTimestamp}", e.LocalTimestamp);
        return Task.CompletedTask;
    }

    public async Task ClosedHandler(object sender, ConnectionChangeEventArgs e)
    {
        // Loggen
        _logger.LogInformation("LiveScout closed at {Time:dd.MM.yyyy hh:mm:ss}", e.LocalTimestamp);
        
        // Handler abrufen
        var eventHandlers = GetEventHandlers<ILiveScoutClosedHandler>();
        
        // Alle Handler informieren
        foreach (var handler in eventHandlers) await handler.Handle(e.LocalTimestamp);
    }

    public async Task MatchBookingReplyHandler(object sender, MatchBookingReplyEventArgs e)
    {
        // Loggen
        _logger.Log(e.MatchBooking.Result == Sportradar.LiveData.Sdk.FeedProviders.LiveScout.Enums.BookMatchResult.VALID ? LogLevel.Information : LogLevel.Error, "Match {MatchId}: {Message}", e.MatchBooking.MatchId, e.MatchBooking.Message);
        
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
        foreach (var handler in eventHandlers) await handler.Handle(matchBookingReply);
    }

    public async Task MatchDataHandler(object sender, MatchDataEventArgs e)
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
        foreach (var handler in eventHandlers) await handler.Handle(data);
    }

    public async Task MatchListHandler(object sender, MatchListEventArgs e)
    {
        var matches = e.MatchList?
            .Select(item => new MatchListItem
            {
                MatchId = item.MatchHeader.MatchId,
                T1Name = item.MatchHeader?.Team1?.Name?.International,
                T2Name = item.MatchHeader?.Team2?.Name?.International,
                CourtName = item.Court?.Name,
                TournamentName = item.Tournament?.Name?.International,
                MatchStatus = (ScoutMatchStatus)(int)item.MatchStatus
            })
            .ToList() ?? new List<MatchListItem>();

        _logger.LogInformation("These are the available matches: {Result}", matches.ToString());

        // Handler abrufen
        var eventHandlers = GetEventHandlers<ILiveScoutMatchListHandler>();
        
        // Alle Handler informieren
        foreach (var handler in eventHandlers) await handler.Handle(matches);
    }

    public async Task MatchStopHandler(object sender, MatchStopEventArgs e)
    {
        _logger.LogInformation("The following match was stopped: {MatchId}", e.MatchId);

        var stop = new MatchStop
        {
            MatchId = e.MatchId,
            Reason = e.Reason
        };

        // Handler abrufen
        var eventHandlers = GetEventHandlers<ILiveScoutMatchStopHandler>();
        
        // Alle Handler informieren
        foreach (var handler in eventHandlers) await handler.Handle(stop);
    }

    public async Task MatchUpdateHandler(object sender, MatchUpdateEventArgs e)
    {
        var mu = e.MatchUpdate;
        var update = new MatchUpdate
        {
            CourtName = mu.Court?.Name,
            MatchId = mu.MatchHeader.MatchId,
            Serve = (Team)(int)mu.Serve,
            T1Name = mu.MatchHeader?.Team1?.Name?.International,
            T2Name = mu.MatchHeader?.Team2?.Name?.International,
            TournamentName = mu.Tournament?.Name?.International
        };

        // Handler abrufen
        var eventHandlers = GetEventHandlers<ILiveScoutMatchUpdateHandler>();
        
        // Alle Handler informieren
        foreach (var handler in eventHandlers) await handler.Handle(update);
    }

    public async Task MatchUpdateDeltaHandler(object sender, MatchUpdateEventArgs e)
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
        foreach (var handler in eventHandlers) await handler.Handle(update);
    }

    public async Task MatchUpdateDeltaUpdateHandler(object sender, MatchUpdateEventArgs e)
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
        foreach (var handler in eventHandlers) await handler.Handle(update);
    }

    public async Task MatchUpdateFullHandler(object sender, MatchUpdateEventArgs e)
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
        foreach (var handler in eventHandlers) await handler.Handle(update);
    }

    public Task FeedErrorHandler(object sender, FeedErrorEventArgs e)
    {
        _logger?.LogError("Error: {ErrorMessage} {Cause} at {Timestamp}", e.ErrorMessage, e.Cause, e.LocalTimestamp);
        return Task.CompletedTask;
    }

    #endregion
}