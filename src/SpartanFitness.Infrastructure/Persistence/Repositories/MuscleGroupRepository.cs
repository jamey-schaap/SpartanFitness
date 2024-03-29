using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using SpartanFitness.Application.Common.Interfaces.Persistence;
using SpartanFitness.Domain.Aggregates;
using SpartanFitness.Domain.ValueObjects;

namespace SpartanFitness.Infrastructure.Persistence.Repositories;

public class MuscleGroupRepository : IMuscleGroupRepository
{
  private readonly SpartanFitnessDbContext _dbContext;

  public MuscleGroupRepository(SpartanFitnessDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task AddAsync(MuscleGroup muscleGroup)
  {
    _dbContext.Add(muscleGroup);

    await _dbContext.SaveChangesAsync();
  }

  public async Task<List<MuscleGroup>> GetBySearchQueryAsync(string searchQuery)
  {
    string query = searchQuery.ToLower();

    return await _dbContext.MuscleGroups
      .Where(mg => (mg.Name.ToLower().Contains(query) || mg.Description.ToLower().Contains(query)))
      .ToListAsync();
  }

  public async Task<List<MuscleGroup>> GetBySearchQueryAndIdsAsync(string searchQuery, List<MuscleGroupId> ids)
  {
    if (!ids.Any())
    {
      return new();
    }

    var searchQueryParam = $"@p{0}";
    var idParameters = string.Join(", ", ids.Select((_, i) => $"@p{i + 1}"));
    var query = $@"
      SELECT *
      FROM MuscleGroups 
      WHERE Id in ({idParameters}) 
        and (LOWER(Name) LIKE '%' + {searchQueryParam} + '%' 
          or LOWER(Description) LIKE '%' + {searchQueryParam} + '%')
    ";

    var sqlSearchParameter = new SqlParameter($"@p{0}", searchQuery.ToLower());
    var sqlParameters = ids
      .Select((id, i) => new SqlParameter($"@p{i + 1}", id.Value))
      .ToList();
    sqlParameters.Add(sqlSearchParameter);

    return await _dbContext.MuscleGroups
      .FromSqlRaw(query, sqlParameters.ToArray())
      .ToListAsync();
  }

  public async Task<bool> ExistsAsync(MuscleGroupId id)
  {
    return await _dbContext.MuscleGroups.AnyAsync(mg => mg.Id == id);
  }

  public async Task<bool> ExistsAsync(IEnumerable<MuscleGroupId> ids)
  {
    var result = new List<bool>();
    foreach (var id in ids)
    {
      result.Add(await _dbContext.MuscleGroups.AnyAsync(mg => mg.Id == id));
    }

    return !result.Contains(false);
  }

  public async Task UpdateAsync(MuscleGroup muscleGroup)
  {
    _dbContext.Update(muscleGroup);
    await _dbContext.SaveChangesAsync();
  }

  public async Task<MuscleGroup?> GetByIdAsync(MuscleGroupId id)
  {
    return await _dbContext.MuscleGroups.FirstOrDefaultAsync(mg => mg.Id == id);
  }

  public async Task<List<MuscleGroup>> GetByIdAsync(List<MuscleGroupId> ids)
  {
    return await _dbContext.MuscleGroups.Where(mg => ids.Contains(mg.Id)).ToListAsync();
  }

  public async Task<MuscleGroup?> GetByNameAsync(string name)
  {
    return await _dbContext.MuscleGroups.FirstOrDefaultAsync(mg => mg.Name == name);
  }

  public async Task<IEnumerable<MuscleGroup>> GetAllAsync()
  {
    return await _dbContext.MuscleGroups.ToListAsync();
  }

  public async Task<List<MuscleGroup>> GetByMuscleIdAsync(MuscleId id)
  {
    return await _dbContext.MuscleGroups.Where(mg => mg.MuscleIds.Contains(id)).ToListAsync();
  }

  public async Task<List<MuscleGroup>> GetByMuscleIdAsync(List<MuscleId> ids)
  {
    // The argument 'ids' throw the following exception if not converted to GUID:
    // "No backing field could be found for property 'MuscleGroup.MuscleIds#MuscleId.Id' and the property does not have a getter."
    var rawIds = ids.ConvertAll(id => id.Value);

    return await _dbContext.MuscleGroups
      .Where(mg => mg.MuscleIds
        .Any(id => rawIds.Contains(id.Value)))
      .ToListAsync();
  }
}