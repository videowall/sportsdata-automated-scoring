using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WBH.Livescoring.Frontend.Entities;

namespace WBH.Livescoring.Frontend.DataAccessLayer.Mappings;

internal sealed class MatchMapping : KeyMappingBase<Match, long>
{
    #region EntityMappingBase

    protected override void AddPropertyMappings(EntityTypeBuilder<Match> entity)
    {
        entity.Property(e => e.Court)
            .HasMaxLength(100)
            .IsRequired(false);

        entity.Property(e => e.TournamentId)
            .IsRequired(false);

        entity.Property(e => e.TournamentName)
            .HasMaxLength(100)
            .IsRequired(false);

        entity.Property(e => e.Service)
            .HasConversion<EnumToStringConverter<HomeAway>>()
            .IsRequired();

        entity.Property(e => e.Status)
            .HasConversion<EnumToStringConverter<Status>>()
            .IsRequired();

        entity.Property(e => e.MatchTime)
            .HasMaxLength(20)
            .IsRequired(false);

        entity.Property(e => e.Team1Line1)
            .HasMaxLength(100)
            .IsRequired(false);

        entity.Property(e => e.Team1Line2)
            .HasMaxLength(100)
            .IsRequired(false);

        entity.Property(e => e.Team2Line1)
            .HasMaxLength(100)
            .IsRequired(false);

        entity.Property(e => e.Team2Line2)
            .HasMaxLength(100)
            .IsRequired(false);
    }

    #endregion
}