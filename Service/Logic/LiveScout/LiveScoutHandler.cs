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
        _logger.LogInformation("Connection Opened");
    }

    public void ClosedHandler(object sender, ConnectionChangeEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void LineupsHandler(object sender, LineupsEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void MatchBookingReplyHandler(object sender, MatchBookingReplyEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void MatchDataHandler(object sender, MatchDataEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void MatchListHandler(object sender, MatchListEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void MatchListUpdateHandler(object sender, MatchListEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void MatchStopHandler(object sender, MatchStopEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void MatchUpdateHandler(object sender, MatchUpdateEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void MatchUpdateDeltaHandler(object sender, MatchUpdateEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void MatchUpdateDeltaUpdateHandler(object sender, MatchUpdateEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void MatchUpdateFullHandler(object sender, MatchUpdateEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OddsSuggestionHandler(object sender, OddsSuggestionEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void ScoutInfoHandler(object sender, ScoutInfoEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void FeedErrorHandler(object sender, FeedErrorEventArgs e)
    {
        throw new NotImplementedException();
    }

    #endregion
}