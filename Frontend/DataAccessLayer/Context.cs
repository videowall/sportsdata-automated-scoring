using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WBH.Livescoring.Frontend.DataAccessLayer.Primitives;

namespace WBH.Livescoring.Frontend.DataAccessLayer;

internal sealed class Context : DbContext, IContext
{
    #region IContext

    IQueryable<TEntity> IContext.Query<TEntity>() => Set<TEntity>().AsQueryable();

    #endregion

    #region DbContext

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AppDomain.CurrentDomain.GetAssemblies().Single(i => i.GetName().Name == typeof(Context).Namespace));
    }

    #endregion
}