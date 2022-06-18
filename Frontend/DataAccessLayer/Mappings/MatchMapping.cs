using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WBH.Livescoring.Frontend.DataAccessLayer.Mappings;

internal sealed class MatchMapping : KeyMappingBase<Entities.Match, long>
{
    #region EntityMappingBase

    protected override void AddPropertyMappings(EntityTypeBuilder<Entities.Match> entity)
    {
        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();
    }

    #endregion
}