using System;
using Microsoft.Extensions.Logging;
using Sportradar.LiveData.Sdk.FeedProviders.Common.Events;
using Sportradar.LiveData.Sdk.FeedProviders.LiveScout.Events;

namespace WBH.Livescoring.Service.Logic.LiveScout;

internal sealed class LiveScoutHandler
{
    #region Fields

    private readonly ILogger _logger;

    #endregion

    #region Constructors

    public LiveScoutHandler(Func<object, ILogger> loggingFactory)
    {
        _logger = loggingFactory(this);
    }

    #endregion

    #region Methods

    public void OnOpened(object sender, ConnectionChangeEventArgs e)
    {
        _logger.LogInformation("Connection Opened at {timestamp}", e.LocalTimestamp);
    }

    public void ClosedHandler(object sender, ConnectionChangeEventArgs e)
    {
        _logger.LogInformation("Connection Closed at {timestamp}", e.LocalTimestamp);
    }

    public void LineupsHandler(object sender, LineupsEventArgs e)
    {
        _logger.LogDebug("{name}: {e}", nameof(LineupsHandler), e);
    }

    public void MatchBookingReplyHandler(object sender, MatchBookingReplyEventArgs e)
    {
        _logger.LogInformation("Match {MatchId} booked: {Message}", e.MatchBooking.MatchId, e.MatchBooking.Message);
    }

    public void MatchDataHandler(object sender, MatchDataEventArgs e)
    {
        _logger.LogDebug("{name}: {e}", nameof(MatchDataHandler), e);
    }

    public void MatchListHandler(object sender, MatchListEventArgs e)
    {
        _logger.LogDebug("{name}: {e}", nameof(MatchListHandler), e);
    }

    public void MatchListUpdateHandler(object sender, MatchListEventArgs e)
    {
        _logger.LogDebug("{name}: {e}", nameof(MatchListUpdateHandler), e);
    }

    public void MatchStopHandler(object sender, MatchStopEventArgs e)
    {
        _logger.LogInformation("Match {MatchId} stopped: {Reason}", e.MatchId, e.Reason);
    }

    public void MatchUpdateHandler(object sender, MatchUpdateEventArgs e)
    {
        _logger.LogDebug("{name}: {e}", nameof(MatchUpdateHandler), e);
        // TODO: Implementierung
    }

    public void MatchUpdateDeltaHandler(object sender, MatchUpdateEventArgs e)
    {
        _logger.LogDebug("{name}: {e}", nameof(MatchUpdateDeltaHandler), e);
        // TODO: Implementierung
    }

    public void MatchUpdateDeltaUpdateHandler(object sender, MatchUpdateEventArgs e)
    {
        _logger.LogDebug("{name}: {e}", nameof(MatchUpdateDeltaUpdateHandler), e);
        // TODO: Implementierung
    }

    public void MatchUpdateFullHandler(object sender, MatchUpdateEventArgs e)
    {
        _logger.LogDebug("{name}: {e}", nameof(MatchUpdateFullHandler), e);
        // TODO: Implementierung
    }

    public void OddsSuggestionHandler(object sender, OddsSuggestionEventArgs e)
    {
        _logger.LogDebug("{name}: {e}", nameof(OddsSuggestionHandler), e);
    }

    public void ScoutInfoHandler(object sender, ScoutInfoEventArgs e)
    {
        _logger.LogDebug("{name}: {e}", nameof(ScoutInfoHandler), e);
    }

    public void FeedErrorHandler(object sender, FeedErrorEventArgs e)
    {
        _logger.LogError("Error: {ErrorMessage} {Cause} at {Timestamp}", e.ErrorMessage, e.Cause, e.LocalTimestamp);
    }

    #endregion
}