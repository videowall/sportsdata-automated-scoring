using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WBH.Livescoring.Frontend.DataAccessLayer.Primitives;

namespace WBH.Livescoring.Frontend.DataAccessLayer;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions
{
    #region Methods

    public static IServiceCollection AddDbContext(
        this IServiceCollection container,
        Action<DbContextOptionsBuilder> optionsAction = null,
        ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
        ServiceLifetime optionsLifetime = ServiceLifetime.Scoped
        ) => container.AddDbContext<IContext, Context>(optionsAction, contextLifetime, optionsLifetime);

    #endregion
}