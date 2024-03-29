using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SpartanFitness.Application.Workouts.Commands.CreateWorkout;
using SpartanFitness.Application.Workouts.Commands.DeleteWorkout;
using SpartanFitness.Application.Workouts.Commands.UpdateWorkout;
using SpartanFitness.Application.Workouts.Queries.GetAllWorkouts;
using SpartanFitness.Application.Workouts.Queries.GetWorkoutById;
using SpartanFitness.Application.Workouts.Queries.GetWorkoutPage;
using SpartanFitness.Contracts.Common;
using SpartanFitness.Contracts.Workouts;
using SpartanFitness.Domain.Aggregates;
using SpartanFitness.Domain.Common.Models;
using SpartanFitness.Domain.Enums;

namespace SpartanFitness.Api.Controllers.V1;

[Route("api/v{version:apiVersion}/coaches/{coachId}/[controller]")]
public class WorkoutsController : ApiController
{
  private readonly IMapper _mapper;
  private readonly ISender _mediator;

  public WorkoutsController(
    IMapper mapper,
    ISender mediator)
  {
    _mapper = mapper;
    _mediator = mediator;
  }

  [HttpGet("{workoutId}")]
  [AllowAnonymous]
  public async Task<IActionResult> GetWorkout(string coachId, string workoutId)
  {
    var query = new GetWorkoutByIdQuery(coachId, workoutId);
    ErrorOr<Workout> workoutResult = await _mediator.Send(query);

    return workoutResult.Match(
      workout => Ok(_mapper.Map<WorkoutResponse>(workout)),
      errors => Problem(errors));
  }

  [HttpGet("/api/v{version:apiVersion}/coaches/all/[controller]")]
  [AllowAnonymous]
  public async Task<IActionResult> GetAllWorkouts()
  {
    var query = new GetAllWorkoutsQuery();
    ErrorOr<List<Workout>> workoutResult = await _mediator.Send(query);

    return workoutResult.Match(
      workout => Ok(_mapper.Map<List<WorkoutResponse>>(workout)),
      errors => Problem(errors));
  }

  [HttpGet("/api/v{version:apiVersion}/coaches/all/[controller]/page/{p:int?}/{ls:int?}/{s?}/{o?}/{q?}")]
  [AllowAnonymous]

  public async Task<IActionResult> GetWorkoutsPage([FromQuery] PagingRequest request)
  {
    var query = _mapper.Map<GetWorkoutPageQuery>(request);
    ErrorOr<Pagination<Workout>> result = await _mediator.Send(query);

    return result.Match(
     result => Ok(_mapper.Map<WorkoutPageResponse>(result)),
     Problem);
  }

  [HttpPost]
  [Authorize(Roles = RoleTypes.Coach)]
  public async Task<IActionResult> CreateWorkout(CreateWorkoutRequest request, string coachId)
  {
    var isCoach = Authorization.CoachIdMatchesClaim(HttpContext, coachId);
    if (!isCoach)
    {
      return Unauthorized();
    }

    var command = _mapper.Map<CreateWorkoutCommand>((request, coachId));
    ErrorOr<Workout> workoutResult = await _mediator.Send(command);

    return workoutResult.Match(
      workout => CreatedAtAction(
        nameof(GetWorkout),
        new { coachId = workout.CoachId.Value, workoutId = workout.Id.Value },
        _mapper.Map<WorkoutResponse>(workout)),
      errors => Problem(errors));
  }

  [HttpPut("{workoutId}")]
  [Authorize(Roles = $"{RoleTypes.Coach}, {RoleTypes.Administrator}")]
  public async Task<IActionResult> UpdateWorkout([FromRoute] string coachId, [FromRoute] string workoutId, [FromBody] UpdateWorkoutRequest request)
  {
    var isAuthorized = Authorization.CoachIdMatchesClaim(HttpContext, coachId) || Authorization.IsAdmin(HttpContext);
    if (!isAuthorized)
    {
      return Unauthorized();
    }

    if (request.Id != workoutId)
    {
      return BadRequest("Route ID must match the ID field in the request body.");
    }

    var command = _mapper.Map<UpdateWorkoutCommand>((request, coachId)) with { Image = request.Image };
    ErrorOr<Workout> result = await _mediator.Send(command);

    return result.Match(
      workout => Ok(_mapper.Map<WorkoutResponse>(workout)),
      Problem);
  }

  [HttpDelete("{workoutId}")]
  [Authorize(Roles = $"{RoleTypes.Coach}, {RoleTypes.Administrator}")]
  public async Task<IActionResult> DeleteWorkout([FromRoute] string coachId, [FromRoute] string workoutId)
  {
    var isAuthorized = Authorization.CoachIdMatchesClaim(HttpContext, coachId) || Authorization.IsAdmin(HttpContext);
    if (!isAuthorized)
    {
      return Unauthorized();
    }

    var command = new DeleteWorkoutCommand(
      CoachId: coachId,
      WorkoutId: workoutId);
    ErrorOr<Unit> result = await _mediator.Send(command);

    return result.Match(
      _ => NoContent(),
      Problem);
  }
}