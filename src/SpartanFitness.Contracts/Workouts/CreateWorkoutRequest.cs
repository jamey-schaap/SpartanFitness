namespace SpartanFitness.Contracts.Workouts;

public record CreateWorkoutRequest(
  string Name,
  string Description,
  string Image,
  List<CreateWorkoutExerciseRequest>? WorkoutExercises);

public record CreateWorkoutExerciseRequest(
  string ExerciseId,
  uint OrderNumber,
  uint Sets,
  uint MinReps,
  uint MaxReps,
  string ExerciseType);