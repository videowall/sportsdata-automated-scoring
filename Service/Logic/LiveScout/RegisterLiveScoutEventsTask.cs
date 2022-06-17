using System;
using Microsoft.Extensions.Logging;
using Sportradar.LiveData.Sdk.FeedProviders.LiveScout.Interfaces;
using WBH.Livescoring.Service.IoC;

namespace WBH.Livescoring.Service.Logic.LiveScout;

internal sealed class RegisterLiveScoutEventsTask : IStartupTask
{
    #region Fields

    private readonly ILiveScout _liveScout;
    private readonly LiveScoutHandler _handler;
    private readonly ILogger _logger;

    #endregion

    #region Constructors

    public RegisterLiveScoutEventsTask(ILiveScout liveScout, LiveScoutHandler handler, Func<object, ILogger> loggingFactory)
    {
        _liveScout = liveScout;
        _handler = handler;
        _logger = loggingFactory(this);
    }

    #endregion

    #region IStartupTask

    public void Start()
    {
        _logger.LogDebug("Register LiveScout Events");
        _liveScout.OnOpened += _handler.OnOpened;
        _liveScout.OnClosed += _handler.ClosedHandler;
        _liveScout.OnLineups += _handler.LineupsHandler;
        _liveScout.OnMatchBookingReply += _handler.MatchBookingReplyHandler;
        _liveScout.OnMatchData += _handler.MatchDataHandler;
        _liveScout.OnMatchList += _handler.MatchListHandler;
        _liveScout.OnMatchListUpdate += _handler.MatchListUpdateHandler;
        _liveScout.OnMatchStop += _handler.MatchStopHandler;
        _liveScout.OnMatchUpdate += _handler.MatchUpdateHandler;
        _liveScout.OnMatchUpdateDelta += _handler.MatchUpdateDeltaHandler;
        _liveScout.OnMatchUpdateDeltaUpdate += _handler.MatchUpdateDeltaUpdateHandler;
        _liveScout.OnMatchUpdateFull += _handler.MatchUpdateFullHandler;
        _liveScout.OnOddsSuggestion += _handler.OddsSuggestionHandler;
        _liveScout.OnScoutInfo += _handler.ScoutInfoHandler;
        _liveScout.OnFeedError += _handler.FeedErrorHandler;
    }

    #endregion
}