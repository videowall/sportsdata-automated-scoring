using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace WBH.Livescoring.Frontend.API;

public sealed partial class Module
{
    #region Methods

    private static void RegisterAutoMapper(IServiceCollection container)
    {
        var profileType = typeof(AutoMapper.Profile);
        container.AddAutoMapper(container.Where(s => profileType.IsAssignableFrom(s.ImplementationType)).Select(s => s.ImplementationType).ToArray());
    }

    #endregion
}