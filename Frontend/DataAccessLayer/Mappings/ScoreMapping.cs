using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WBH.Livescoring.Frontend.Entities;

namespace WBH.Livescoring.Frontend.DataAccessLayer.Mappings;

public class ScoreMapping: EntityMappingBase<Entities.Score>
{
    #region EntityMappingBase

    protected override void AddKeyMapping(EntityTypeBuilder<Score> entity)
    {
        entity.HasKey(e => new { e.MatchId, e.Type });
    }

    protected override void AddPropertyMappings(EntityTypeBuilder<Entities.Score> entity)
    {
        entity.Property(e => e.Type)
            .HasConversion<EnumToStringConverter<ScoreType>>()
            .IsRequired();

        entity.Property(e => e.MatchId).IsRequired();
        
        
        entity.Property(e => e.Team1)
            .IsRequired();
        
        entity.Property(e => e.Team2)
            .IsRequired();

    }

    protected override void AddNavigationProperties(EntityTypeBuilder<Score> entity)
    {
        entity.HasOne(e => e.MatchNavigation)
            .WithMany(d => d.Scores)
            .HasForeignKey(e => e.MatchId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    #endregion
}