﻿using System;
using System.Linq;
using System.Threading.Tasks;
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
        if (entity == null) return;
        
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

    async Task IContext.SaveAsync<TEntity>(TEntity entity)
    {
        if (entity == null) return;
        
        var entry = Entry(entity);
        if (await entry.GetDatabaseValuesAsync() == null || entry.State == EntityState.Detached)
        {
            await AddAsync(entity);
        }
        else
        {
            Update(entity);
        }

        await SaveChangesAsync();
    }

    async Task IContext.Delete<TEntity>(TEntity entity)
    {
        Remove(entity);
        await SaveChangesAsync();
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