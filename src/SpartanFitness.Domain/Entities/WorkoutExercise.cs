using SpartanFitness.Domain.Common.Models;
using SpartanFitness.Domain.Enums;
using SpartanFitness.Domain.ValueObjects;

namespace SpartanFitness.Domain.Entities;

public sealed class WorkoutExercise : Entity<WorkoutExerciseId>
{
  public uint OrderNumber { get; private set; }
  public ExerciseId ExerciseId { get; private set; }
  public uint Sets { get; private set; }
  public RepRange RepRange { get; private set; }
  public ExerciseType ExerciseType { get; private set; }

  private WorkoutExercise(
    WorkoutExerciseId id,
    uint orderNumber,
    ExerciseId exerciseId,
    uint sets,
    RepRange repRange,
    ExerciseType exerciseType)
    : base(id)
  {
    OrderNumber = orderNumber;
    ExerciseId = exerciseId;
    Sets = sets;
    RepRange = repRange;
    ExerciseType = exerciseType;
  }

#pragma warning disable CS8618
  private WorkoutExercise()
  {
  }
#pragma warning restore CS8618

  public static WorkoutExercise Create(
    uint orderNumber,
    ExerciseId exerciseId,
    uint sets,
    RepRange repRange,
    ExerciseType exerciseType)
  {
    return new(
      WorkoutExerciseId.CreateUnique(),
      orderNumber,
      exerciseId,
      sets,
      repRange,
      exerciseType);
  }

  public void SetOrderNumber(uint orderNumber) => OrderNumber = orderNumber;
  public void SetExerciseId(ExerciseId id) => ExerciseId = id;
  public void SetSets(uint sets) => Sets = sets;
  public void SetRepRange(RepRange repRange) => RepRange = repRange;
  public void SetExerciseType(ExerciseType exerciseType) => ExerciseType = exerciseType;
}