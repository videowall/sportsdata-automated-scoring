using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WBH.Livescoring.Frontend.Entities;

namespace WBH.Livescoring.Frontend.DataAccessLayer.Mappings;

public abstract class EntityMappingBase<TEntity>: IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
{
    #region IEntityTypeConfiguration

    void IEntityTypeConfiguration<TEntity>.Configure(EntityTypeBuilder<TEntity> builder)
    {
        AddDefaultMappings(builder);
    } 
            
    #endregion

    #region Methods

    protected abstract void AddPropertyMappings(EntityTypeBuilder<TEntity> entity);

    protected virtual void AddNavigationProperties(EntityTypeBuilder<TEntity> entity)
    {
    }

    protected virtual void AddIgnoredProperties(EntityTypeBuilder<TEntity> entity)
    {
    }

    protected virtual void AddKeyMapping(EntityTypeBuilder<TEntity> entity)
    {
    }
            
    protected virtual void AddQueryFilters(EntityTypeBuilder<TEntity> entity)
    {
    }

    private void AddDefaultMappings(EntityTypeBuilder<TEntity> entity)
    {
        AddKeyMapping(entity);

        AddPropertyMappings(entity);
        AddNavigationProperties(entity);
        AddIgnoredProperties(entity);
        AddQueryFilters(entity);
    }

    #endregion
}