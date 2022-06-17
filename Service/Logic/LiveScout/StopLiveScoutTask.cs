using System;
using Microsoft.Extensions.Logging;
using Sportradar.LiveData.Sdk.FeedProviders.LiveScout.Interfaces;
using Sportradar.LiveData.Sdk.Services;
using WBH.Livescoring.Service.IoC;

namespace WBH.Livescoring.Service.Logic.LiveScout;

internal sealed class StopLiveScoutTask : IShutdownTask
{
    #region Fields

    private readonly Sdk _sdk;
    private readonly ILiveScout _liveScout;
    private readonly ILogger _logger;

    #endregion

    #region Constructors

    public StopLiveScoutTask(Sdk sdk, ILiveScout liveScout, Func<object, ILogger> loggingFactory)
    {
        _sdk = sdk;
        _liveScout = liveScout;
        _logger = loggingFactory(this);
    }

    #endregion

    #region IShutdownTask

    public void Shutdown()
    {
        _logger.LogDebug("Shutdown Sportradar");
        _liveScout.Stop();
        _sdk.Stop();
    }

    #endregion
}