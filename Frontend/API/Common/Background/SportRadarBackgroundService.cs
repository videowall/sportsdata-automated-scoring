using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WBH.Livescoring.SportRadar;

namespace WBH.Livescoring.Frontend.API.Common.Background;

internal sealed class SportRadarBackgroundService : IHostedService
{
    #region Constructors

    public SportRadarBackgroundService(ILiveScoutService service, ILogger<SportRadarBackgroundService> logger)
    {
        _service = service;
        _logger = logger;
    }

    #endregion

    #region Fields

    private readonly ILiveScoutService _service;
    private readonly ILogger<SportRadarBackgroundService> _logger;

    #endregion

    #region IHostedService

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Start SportRadar");
        _service.Start();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Stop SportRadar");
        _service.Stop();
        return Task.CompletedTask;
    }

    #endregion
}