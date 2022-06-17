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
    }

    #endregion
}