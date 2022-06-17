using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Sportradar.LiveData.Sdk.FeedProviders.Common.Enums;
using Sportradar.LiveData.Sdk.FeedProviders.Common.Events;
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
        _logger?.LogInformation("Connection Opened at {timestamp}", e.LocalTimestamp);

        // Alle Handler informieren
        foreach (var handler in _eventHandlers.OfType<ILiveScoutOnOpenedEventHandler>())
        {
            handler.Handle(e.LocalTimestamp);
        }
    }

    public void ClosedHandler(object sender, ConnectionChangeEventArgs e)
    {
        _logger?.LogInformation("Connection Closed at {timestamp}", e.LocalTimestamp);
        
        foreach (var handler in _eventHandlers.OfType<ILiveScoutClosedHandler>())
        {
            handler.Handle(e.LocalTimestamp);
        }
    }

    public void LineupsHandler(object sender, LineupsEventArgs e)
    {
        _logger?.LogDebug("{name}: {e}", nameof(LineupsHandler), e);
        //NICHT RELEVANT
    }

    public void MatchBookingReplyHandler(object sender, MatchBookingReplyEventArgs e)
    {
        _logger?.LogInformation("Match {MatchId} booked: {Message}", e.MatchBooking.MatchId, e.MatchBooking.Message);
        
        foreach (var handler in _eventHandlers.OfType<ILiveScoutMatchBookingReplyHandler>())
        {
            handler.Handle(e.MatchBooking.MatchId, e.MatchBooking.Message, e.MatchBooking.Result);
        }
    }

    public void MatchDataHandler(object sender, MatchDataEventArgs e)
    {
        _logger?.LogDebug("{name}: {e}", nameof(MatchDataHandler), e);
    }

    public void MatchListHandler(object sender, MatchListEventArgs e)
    {
        _logger?.LogDebug("{name}: {e}", nameof(MatchListHandler), e);
    }

    public void MatchListUpdateHandler(object sender, MatchListEventArgs e)
    {
        _logger?.LogDebug("{name}: {e}", nameof(MatchListUpdateHandler), e);
    }

    public void MatchStopHandler(object sender, MatchStopEventArgs e)
    {
        _logger?.LogInformation("Match {MatchId} stopped: {Reason}", e.MatchId, e.Reason);
    }

    public void MatchUpdateHandler(object sender, MatchUpdateEventArgs e)
    {
        _logger?.LogDebug("{name}: {e}", nameof(MatchUpdateHandler), e);
        // TODO: Implementierung
    }

    public void MatchUpdateDeltaHandler(object sender, MatchUpdateEventArgs e)
    {
        _logger?.LogDebug("{name}: {e}", nameof(MatchUpdateDeltaHandler), e);
        // TODO: Implementierung
    }

    public void MatchUpdateDeltaUpdateHandler(object sender, MatchUpdateEventArgs e)
    {
        _logger?.LogDebug("{name}: {e}", nameof(MatchUpdateDeltaUpdateHandler), e);
        // TODO: Implementierung
    }

    public void MatchUpdateFullHandler(object sender, MatchUpdateEventArgs e)
    {
        _logger?.LogDebug("{name}: {e}", nameof(MatchUpdateFullHandler), e);
        // TODO: Implementierung
    }

    public void OddsSuggestionHandler(object sender, OddsSuggestionEventArgs e)
    {
        _logger?.LogDebug("{name}: {e}", nameof(OddsSuggestionHandler), e);
        //NICHT RELEVANT
    }

    public void ScoutInfoHandler(object sender, ScoutInfoEventArgs e)
    {
        _logger?.LogDebug("{name}: {e}", nameof(ScoutInfoHandler), e);
        //NICHT RELEVANT
    }

    public void FeedErrorHandler(object sender, FeedErrorEventArgs e)
    {
        _logger?.LogError("Error: {ErrorMessage} {Cause} at {Timestamp}", e.ErrorMessage, e.Cause, e.LocalTimestamp);
    }

    #endregion
}