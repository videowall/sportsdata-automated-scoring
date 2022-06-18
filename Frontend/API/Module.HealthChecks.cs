using Microsoft.Extensions.DependencyInjection;

namespace WBH.Livescoring.Frontend.API;

public sealed partial class Module
{
    #region Methods

    private static void RegisterHealthChecks(IServiceCollection container)
    {
        container.AddHealthChecks();
    }

    #endregion
}