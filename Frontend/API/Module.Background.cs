using Microsoft.Extensions.DependencyInjection;
using WBH.Livescoring.Frontend.API.Common.Background;

namespace WBH.Livescoring.Frontend.API;

public sealed partial class Module
{
    #region Methods

    private static void RegisterBackgroundJobs(IServiceCollection container)
    {
        container.AddHostedService<SportRadarBackgroundService>();
    }

    #endregion
}