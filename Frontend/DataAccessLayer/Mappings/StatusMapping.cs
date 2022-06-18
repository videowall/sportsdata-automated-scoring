using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WBH.Livescoring.Frontend.Entities;

namespace WBH.Livescoring.Frontend.DataAccessLayer.Mappings;

public class StatusMapping : KeyMappingBase<Status, long>
{
    #region EntityMappingBase

    protected override void AddPropertyMappings(EntityTypeBuilder<Status> entity)
    {
        entity.Property(e => e.MatchId).IsRequired(false);

        entity.Property(e => e.Message)
            .HasMaxLength(int.MaxValue)
            .IsUnicode()
            .IsRequired();

        entity.Property(e => e.Happened)
            .ValueGeneratedOnAdd()
            .IsRequired();
    }

    protected override void AddNavigationProperties(EntityTypeBuilder<Status> entity)
    {
        entity.HasOne(e => e.MatchNavigation)
            .WithMany(d => d.Status)
            .HasForeignKey(e => e.MatchId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    #endregion
}