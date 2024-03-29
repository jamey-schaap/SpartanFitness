using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SpartanFitness.Domain.Aggregates;
using SpartanFitness.Domain.ValueObjects;

namespace SpartanFitness.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    ConfigureUsersTable(builder);
    ConfigureUserSavedExerciseIdsTable(builder);
    ConfigureUserSavedMuscleIdsTable(builder);
    ConfigureUserSavedMuscleGroupIdsTable(builder);
    ConfigureUserSavedWorkoutIdsTable(builder);
  }

  private void ConfigureUserSavedWorkoutIdsTable(EntityTypeBuilder<User> builder)
  {
    builder.OwnsMany(u => u.SavedWorkoutIds, swib =>
   {
     swib.ToTable("UserSavedWorkoutIds");

     swib.WithOwner().HasForeignKey("UserId");

     swib.HasKey("Id");

     swib.Property(smi => smi.Value)
       .HasColumnName("WorkoutId")
       .ValueGeneratedNever();
   });

    builder.Metadata.FindNavigation(nameof(User.SavedWorkoutIds))!
      .SetPropertyAccessMode(PropertyAccessMode.Field);
  }

  private void ConfigureUserSavedMuscleGroupIdsTable(EntityTypeBuilder<User> builder)
  {
    builder.OwnsMany(u => u.SavedMuscleGroupIds, smgib =>
    {
      smgib.ToTable("UserSavedMuscleGroupIds");

      smgib.WithOwner().HasForeignKey("UserId");

      smgib.HasKey("Id");

      smgib.Property(smi => smi.Value)
        .HasColumnName("MuscleGroupId")
        .ValueGeneratedNever();
    });

    builder.Metadata.FindNavigation(nameof(User.SavedMuscleGroupIds))!
      .SetPropertyAccessMode(PropertyAccessMode.Field);
  }

  private void ConfigureUserSavedMuscleIdsTable(EntityTypeBuilder<User> builder)
  {
    builder.OwnsMany(u => u.SavedMuscleIds, smib =>
    {
      smib.ToTable("UserSavedMuscleIds");

      smib.WithOwner().HasForeignKey("UserId");

      smib.HasKey("Id");

      smib.Property(smi => smi.Value)
        .HasColumnName("MuscleId")
        .ValueGeneratedNever();
    });

    builder.Metadata.FindNavigation(nameof(User.SavedMuscleIds))!
      .SetPropertyAccessMode(PropertyAccessMode.Field);
  }

  private void ConfigureUserSavedExerciseIdsTable(EntityTypeBuilder<User> builder)
  {
    builder.OwnsMany(u => u.SavedExerciseIds, seib =>
    {
      seib.ToTable("UserSavedExerciseIds");

      seib.WithOwner().HasForeignKey("UserId");

      seib.HasKey("Id");

      seib.Property(sei => sei.Value)
        .HasColumnName("ExerciseId")
        .ValueGeneratedNever();
    });

    builder.Metadata.FindNavigation(nameof(User.SavedExerciseIds))!
      .SetPropertyAccessMode(PropertyAccessMode.Field);
  }

  private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("Users");

    builder.HasKey(u => u.Id);

    builder.Property(u => u.Id)
      .ValueGeneratedNever()
      .HasConversion(
        id => id.Value,
        value => UserId.Create(value));

    builder.Property(u => u.FirstName)
      .HasMaxLength(100);

    builder.Property(u => u.LastName)
      .HasMaxLength(100);

    builder.Property(u => u.ProfileImage)
      .HasMaxLength(2048);

    builder.Property(u => u.Salt)
      .ValueGeneratedNever();
  }
}