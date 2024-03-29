using Microsoft.AspNetCore.Http;

using SpartanFitness.Domain.Aggregates;
using SpartanFitness.Domain.ValueObjects;

namespace SpartanFitness.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
  Task<User?> GetByEmailAsync(string email);
  Task<User?> GetByIdAsync(UserId id);
  Task<List<User>> GetByIdAsync(List<UserId> ids);
  Task<User?> GetByCoachIdAsync(CoachId id);
  Task<List<User>> GetByCoachIdAsync(List<CoachId> ids);
  Task<bool> ExistsAsync(UserId id);
  Task AddAsync(User user);
  Task UpdateAsync(User user);
}