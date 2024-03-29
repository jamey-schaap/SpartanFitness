using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SpartanFitness.Domain.Aggregates;
using SpartanFitness.Domain.ValueObjects;

namespace SpartanFitness.Infrastructure.Configuration;

public class MuscleGroupConfiguration : IEntityTypeConfiguration<MuscleGroup>
{
  public void Configure(EntityTypeBuilder<MuscleGroup> builder)
  {
    ConfigureMuscleGroupsTable(builder);
    ConfigureMuscleGroupsMuscleIdsTable(builder);
  }

  private void ConfigureMuscleGroupsMuscleIdsTable(EntityTypeBuilder<MuscleGroup> builder)
  {
    builder.OwnsMany(mg => mg.MuscleIds, mib =>
    {
      mib.ToTable("MuscleGroupMuscleIds");

      mib.WithOwner().HasForeignKey("MuscleGroupId");

      mib.HasKey("Id");

      mib.Property(m => m.Value)
        .HasColumnName("MuscleId")
        .ValueGeneratedNever();
    });

    builder.Metadata.FindNavigation(nameof(MuscleGroup.MuscleIds))!
      .SetPropertyAccessMode(PropertyAccessMode.Field);
  }

  private void ConfigureMuscleGroupsTable(EntityTypeBuilder<MuscleGroup> builder)
  {
    builder.ToTable("MuscleGroups");

    builder.HasKey(mg => mg.Id);

    builder.Property(mg => mg.Id)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => MuscleGroupId.Create(value));

    builder.Property(mg => mg.Description)
      .HasMaxLength(2048);

    builder.Property(mg => mg.Image)
      .HasMaxLength(2048);
  }
}