using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WBH.Livescoring.Frontend.DataAccessLayer;

namespace WBH.Livescoring.Frontend.API;

public sealed partial class Module
{
    #region Methods

    private static void RegisterDataAccessLayer(IServiceCollection container)
    {
        container.AddDbContext(options => options.UseInMemoryDatabase("SportRadar"));
    }

    #endregion
}