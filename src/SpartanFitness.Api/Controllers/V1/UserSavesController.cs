﻿using ErrorOr;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using SpartanFitness.Application.Users.Commands.SaveExercise;
using SpartanFitness.Application.Users.Commands.SaveMuscle;
using SpartanFitness.Application.Users.Commands.SaveMuscleGroup;
using SpartanFitness.Application.Users.Commands.SaveWorkout;
using SpartanFitness.Application.Users.Commands.UnSaveExercise;
using SpartanFitness.Application.Users.Commands.UnSaveMuscle;
using SpartanFitness.Application.Users.Commands.UnSaveMuscleGroup;
using SpartanFitness.Application.Users.Commands.UnSaveWorkout;
using SpartanFitness.Contracts.Users.Saves;

namespace SpartanFitness.Api.Controllers.V1;

[Route("api/v{version:apiVersion}/users/{userId}/saved")]
public class UserSavesController : ApiController
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public UserSavesController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpPatch("exercises/add")]
  public async Task<IActionResult> SaveExercise([FromRoute] string userId, [FromBody] SaveExerciseRequest request)
  {
    var isUser = Authorization.UserIdMatchesClaim(HttpContext, userId);
    if (!isUser)
    {
      return Unauthorized();
    }

    var command = _mapper.Map<SaveExerciseCommand>((request, userId));
    ErrorOr<Unit> result = await _mediator.Send(command);

    return result.Match(
      _ => NoContent(),
      Problem);
  }

  [HttpPatch("exercises/remove")]
  public async Task<IActionResult> UnSaveExercise([FromRoute] string userId, [FromBody] UnSaveExerciseRequest request)
  {
    var isUser = Authorization.UserIdMatchesClaim(HttpContext, userId);
    if (!isUser)
    {
      return Unauthorized();
    }

    var command = _mapper.Map<UnSaveExerciseCommand>((request, userId));
    ErrorOr<Unit> result = await _mediator.Send(command);

    return result.Match(
      _ => NoContent(),
      Problem);
  }

  [HttpPatch("muscle-groups/add")]
  public async Task<IActionResult> SaveMuscleGroup([FromRoute] string userId, [FromBody] SaveMuscleGroupRequest request)
  {
    var isUser = Authorization.UserIdMatchesClaim(HttpContext, userId);
    if (!isUser)
    {
      return Unauthorized();
    }

    var command = _mapper.Map<SaveMuscleGroupCommand>((request, userId));
    ErrorOr<Unit> result = await _mediator.Send(command);

    return result.Match(
      _ => NoContent(),
      Problem);
  }

  [HttpPatch("muscle-groups/remove")]
  public async Task<IActionResult> UnSaveMuscleGroup(
    [FromRoute] string userId,
    [FromBody] UnSaveMuscleGroupRequest request)
  {
    var isUser = Authorization.UserIdMatchesClaim(HttpContext, userId);
    if (!isUser)
    {
      return Unauthorized();
    }

    var command = _mapper.Map<UnSaveMuscleGroupCommand>((request, userId));
    ErrorOr<Unit> result = await _mediator.Send(command);

    return result.Match(
      _ => NoContent(),
      Problem);
  }

  [HttpPatch("muscles/add")]
  public async Task<IActionResult> SaveMuscle([FromRoute] string userId, [FromBody] SaveMuscleRequest request)
  {
    var isUser = Authorization.UserIdMatchesClaim(HttpContext, userId);
    if (!isUser)
    {
      return Unauthorized();
    }

    var command = _mapper.Map<SaveMuscleCommand>((request, userId));
    ErrorOr<Unit> result = await _mediator.Send(command);

    return result.Match(
      _ => NoContent(),
      Problem);
  }

  [HttpPatch("muscles/remove")]
  public async Task<IActionResult> UnSaveMuscle([FromRoute] string userId, [FromBody] UnSaveMuscleRequest request)
  {
    var isUser = Authorization.UserIdMatchesClaim(HttpContext, userId);
    if (!isUser)
    {
      return Unauthorized();
    }

    var command = _mapper.Map<UnSaveMuscleCommand>((request, userId));
    ErrorOr<Unit> result = await _mediator.Send(command);

    return result.Match(
      _ => NoContent(),
      Problem);
  }

  [HttpPatch("workouts/add")]
  public async Task<IActionResult> SaveWorkout([FromRoute] string userId, [FromBody] SaveWorkoutRequest request)
  {
    var isUser = Authorization.UserIdMatchesClaim(HttpContext, userId);
    if (!isUser)
    {
      return Unauthorized();
    }

    var command = _mapper.Map<SaveWorkoutCommand>((request, userId));
    ErrorOr<Unit> result = await _mediator.Send(command);

    return result.Match(
      _ => NoContent(),
      Problem);
  }

  [HttpPatch("workouts/remove")]
  public async Task<IActionResult> UnSaveWorkout([FromRoute] string userId, [FromBody] UnSaveWorkoutRequest request)
  {
    var isUser = Authorization.UserIdMatchesClaim(HttpContext, userId);
    if (!isUser)
    {
      return Unauthorized();
    }

    var command = _mapper.Map<UnSaveWorkoutCommand>((request, userId));
    ErrorOr<Unit> result = await _mediator.Send(command);

    return result.Match(
      _ => NoContent(),
      Problem);
  }
}