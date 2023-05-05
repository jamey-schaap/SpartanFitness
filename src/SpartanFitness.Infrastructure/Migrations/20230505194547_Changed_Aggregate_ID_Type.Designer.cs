﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpartanFitness.Infrastructure.Persistence;

#nullable disable

namespace SpartanFitness.Infrastructure.Migrations
{
    [DbContext(typeof(SpartanFitnessDbContext))]
    [Migration("20230505194547_Changed_Aggregate_ID_Type")]
    partial class Changed_Aggregate_ID_Type
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SpartanFitness.Domain.Aggregates.Administrator", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Administrators", (string)null);
                });

            modelBuilder.Entity("SpartanFitness.Domain.Aggregates.Coach", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Coaches", (string)null);
                });

            modelBuilder.Entity("SpartanFitness.Domain.Aggregates.CoachApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ClosedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Motivation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("CoachApplications", (string)null);
                });

            modelBuilder.Entity("SpartanFitness.Domain.Aggregates.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Video")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("Exercises", (string)null);
                });

            modelBuilder.Entity("SpartanFitness.Domain.Aggregates.Muscle", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MuscleGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Muscles", (string)null);
                });

            modelBuilder.Entity("SpartanFitness.Domain.Aggregates.MuscleGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MuscleGroups", (string)null);
                });

            modelBuilder.Entity("SpartanFitness.Domain.Aggregates.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Invalidated")
                        .HasColumnType("bit");

                    b.Property<string>("JwtId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Used")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens", (string)null);
                });

            modelBuilder.Entity("SpartanFitness.Domain.Aggregates.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImage")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("SpartanFitness.Domain.Aggregates.Workout", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CoachId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Workouts", (string)null);
                });

            modelBuilder.Entity("SpartanFitness.Domain.Aggregates.Exercise", b =>
                {
                    b.OwnsMany("SpartanFitness.Domain.ValueObjects.MuscleGroupId", "MuscleGroupIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<Guid>("ExerciseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("MuscleGroupId");

                            b1.HasKey("Id");

                            b1.HasIndex("ExerciseId");

                            b1.ToTable("ExerciseMuscleGroupIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ExerciseId");
                        });

                    b.OwnsMany("SpartanFitness.Domain.ValueObjects.MuscleId", "MuscleIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<Guid>("ExerciseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("MuscleId");

                            b1.HasKey("Id");

                            b1.HasIndex("ExerciseId");

                            b1.ToTable("ExerciseMuscleIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ExerciseId");
                        });

                    b.Navigation("MuscleGroupIds");

                    b.Navigation("MuscleIds");
                });

            modelBuilder.Entity("SpartanFitness.Domain.Aggregates.Workout", b =>
                {
                    b.OwnsMany("SpartanFitness.Domain.ValueObjects.MuscleGroupId", "MuscleGroupIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("MuscleGroupId");

                            b1.Property<Guid>("WorkoutId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("WorkoutId");

                            b1.ToTable("WorkoutMuscleGroupIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("WorkoutId");
                        });

                    b.OwnsMany("SpartanFitness.Domain.Entities.WorkoutExercise", "WorkoutExercises", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("WorkoutExerciseId");

                            b1.Property<Guid>("WorkoutId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("ExerciseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("ExerciseType")
                                .HasColumnType("int");

                            b1.Property<long>("OrderNumber")
                                .HasColumnType("bigint");

                            b1.Property<long>("Sets")
                                .HasColumnType("bigint");

                            b1.HasKey("Id", "WorkoutId");

                            b1.HasIndex("WorkoutId");

                            b1.ToTable("WorkoutExercises", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("WorkoutId");

                            b1.OwnsOne("SpartanFitness.Domain.ValueObjects.RepRange", "RepRange", b2 =>
                                {
                                    b2.Property<Guid>("WorkoutExerciseId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("WorkoutExerciseWorkoutId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<long>("MaxReps")
                                        .HasColumnType("bigint");

                                    b2.Property<long>("MinReps")
                                        .HasColumnType("bigint");

                                    b2.HasKey("WorkoutExerciseId", "WorkoutExerciseWorkoutId");

                                    b2.ToTable("WorkoutExercises");

                                    b2.WithOwner()
                                        .HasForeignKey("WorkoutExerciseId", "WorkoutExerciseWorkoutId");
                                });

                            b1.Navigation("RepRange")
                                .IsRequired();
                        });

                    b.Navigation("MuscleGroupIds");

                    b.Navigation("WorkoutExercises");
                });
#pragma warning restore 612, 618
        }
    }
}
