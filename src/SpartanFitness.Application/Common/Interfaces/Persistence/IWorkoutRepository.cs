using SpartanFitness.Domain.Aggregates;
using SpartanFitness.Domain.ValueObjects;

namespace SpartanFitness.Application.Common.Interfaces.Persistence;

public interface IWorkoutRepository
{
  Task AddAsync(Workout workout);
  Task<Workout?> GetByIdAsync(WorkoutId id);
  Task<List<Workout>> GetByExerciseId(ExerciseId id);
  Task<IEnumerable<Workout>> GetAllAsync();
  Task<List<Workout>> GetBySearchQueryAsync(string searchQuery);
  Task UpdateAsync(Workout workout);
  Task RemoveAsync(Workout workout);
  Task<List<User>> GetSubscribers(WorkoutId id);
  Task<List<User>> GetSubscribers(List<WorkoutId> ids);
}