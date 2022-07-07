using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WBH.Livescoring.SportRadar;

namespace WBH.Livescoring.Frontend.API.Common.Background;

internal sealed class SportRadarUpdateBackgroundService : IHostedService, IDisposable
{
    #region Constructors

    public SportRadarUpdateBackgroundService(ILiveScoutService service, ILogger<SportRadarUpdateBackgroundService> logger)
    {
        _service = service;
        _logger = logger;
    }

    #endregion

    #region Fields

    private readonly ILiveScoutService _service;
    private readonly ILogger<SportRadarUpdateBackgroundService> _logger;
    private Timer _timer = null;

    #endregion

    #region IHostedService

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Start SportRadar Updater");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(15));
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Stop SportRadar");

        _timer?.Change(Timeout.Infinite, 0);
        
        return Task.CompletedTask;
    }

    #endregion

    #region IDisposable
    
    public void Dispose()
    {
        _timer?.Dispose();
    }

    #endregion

    #region Methods

    private void DoWork(object state)
    {
        _logger.LogDebug("Get Match List");
        _service.GetMatchList(24, 24);
    }

    #endregion
}