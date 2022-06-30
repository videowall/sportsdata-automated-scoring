using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WBH.Livescoring.Frontend.DataAccessLayer.Primitives;

namespace WBH.Livescoring.Frontend.DataAccessLayer;

internal sealed class Context : DbContext, IContext
{
    #region Constructors

    public Context (DbContextOptions<Context> options) : base(options) {}

    #endregion
    
    #region IContext

    IQueryable<TEntity> IContext.Query<TEntity>()
    {
        return Set<TEntity>().AsQueryable();
    }

    void IContext.Save<TEntity>(TEntity entity)
    {
        var entry = Entry(entity);
        if (entry.GetDatabaseValues() == null || entry.State == EntityState.Detached)
        {
            Add(entity);
        }
        else
        {
            Update(entity);
        }

        SaveChanges();
    }

    void IContext.Delete<TEntity>(TEntity entity)
    {
        Remove(entity);
        SaveChanges();
    }

    #endregion

    #region DbContext

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AppDomain.CurrentDomain.GetAssemblies()
            .Single(i => i.GetName().Name == typeof(Context).Namespace));
    }

    #endregion
}