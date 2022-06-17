using System;
using Microsoft.Extensions.Logging;
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

    public void OnOpened(object sender, ConnectionChangeEventArgs eventArgs)
    {
        _logger.LogInformation("Connection Opened");
    }

    #endregion
}