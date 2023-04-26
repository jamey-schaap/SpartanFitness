using ErrorOr;

using MediatR;

using SpartanFitness.Domain.Aggregates;
using SpartanFitness.Domain.Common.Models;

namespace SpartanFitness.Application.Exercises.Queries.GetExercisePage;

public record GetExercisePageQuery(
  int? PageSize,
  int? PageNumber,
  string? Sort,
  string? Order,
  string? SearchQuery) : IRequest<ErrorOr<Page<Exercise>>>;