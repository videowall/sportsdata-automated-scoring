using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WBH.Livescoring.Frontend.Entities;

namespace WBH.Livescoring.Frontend.DataAccessLayer.Mappings;

public abstract class KeyMappingBase<TEntity, TKey> : EntityMappingBase<TEntity> where TEntity : class, IEntity<TKey>
{
    #region Methods

    protected override void AddKeyMapping(EntityTypeBuilder<TEntity> entity)
    {
        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();
    }

    #endregion
}