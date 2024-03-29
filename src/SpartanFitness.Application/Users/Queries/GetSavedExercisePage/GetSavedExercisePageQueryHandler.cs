using ErrorOr;

using MediatR;

using SpartanFitness.Application.Common.Interfaces.Persistence;
using SpartanFitness.Domain.Aggregates;
using SpartanFitness.Domain.Common.Errors;
using SpartanFitness.Domain.Common.Models;
using SpartanFitness.Domain.ValueObjects;

namespace SpartanFitness.Application.Users.Queries.GetSavedExercisePage;

public class GetSavedExercisePageQueryHandler
  : IRequestHandler<GetSavedExercisePageQuery, ErrorOr<Pagination<Exercise>>>
{
  private readonly IUserRepository _userRepository;
  private readonly IExerciseRepository _exerciseRepository;

  public GetSavedExercisePageQueryHandler(
    IUserRepository userRepository,
    IExerciseRepository exerciseRepository)
  {
    _userRepository = userRepository;
    _exerciseRepository = exerciseRepository;
  }

  public async Task<ErrorOr<Pagination<Exercise>>> Handle(
    GetSavedExercisePageQuery query,
    CancellationToken cancellationToken)
  {
    var userId = UserId.Create(query.UserId);
    if (await _userRepository.GetByIdAsync(userId) is not User user)
    {
      return Errors.User.NotFound;
    }

    var exerciseIds = user.SavedExerciseIds.ToList();
    IEnumerable<Exercise> exercises = query.SearchQuery is not null
      ? await _exerciseRepository.GetBySearchQueryAndIdsAsync(query.SearchQuery, exerciseIds)
      : await _exerciseRepository.GetByIdAsync(exerciseIds);

    var pageNumber = query.PageNumber ?? 1;

    exercises = query.Sort switch
    {
      "name" => query.Order switch
      {
        "asc" => exercises.OrderBy(ex => ex.Name),
        "desc" => exercises.OrderByDescending(ex => ex.Name),
        _ => exercises.OrderByDescending(ex => ex.Name),
      },
      "created" => query.Order switch
      {
        "desc" => exercises.OrderByDescending(ex => ex.CreatedDateTime),
        "asc" => exercises.OrderBy(ex => ex.CreatedDateTime),
        _ => exercises.OrderByDescending(ex => ex.CreatedDateTime),
      },
      "updated" => query.Order switch
      {
        "desc" => exercises.OrderByDescending(ex => ex.UpdatedDateTime),
        "asc" => exercises.OrderBy(ex => ex.UpdatedDateTime),
        _ => exercises.OrderByDescending(ex => ex.UpdatedDateTime),
      },
      _ => exercises.OrderByDescending(ex => ex.CreatedDateTime),
    };

    decimal pageCount = query.PageSize == null
      ? 1
      : Math.Ceiling((decimal)exercises.Count() / (int)query.PageSize);

    exercises = exercises
      .Skip((pageNumber - 1) * query.PageSize ?? 0)
      .ToList();

    if (!(pageNumber == 1 && pageCount == 0) &&
        pageNumber > pageCount)
    {
      return Errors.Page.NotFound;
    }

    exercises = query.PageSize == null
      ? exercises
      : exercises
        .Take((int)query.PageSize);

    return new Pagination<Exercise>(
      exercises.ToList(),
      pageNumber,
      (int)pageCount);
  }
}