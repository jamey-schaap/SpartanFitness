﻿namespace SpartanFitness.Contracts.MuscleGroups;

public record MuscleGroupPageResponse(
  List<MuscleGroupPageMusclesResponse> MuscleGroups,
  int PageNumber,
  int PageCount);

public record MuscleGroupPageMusclesResponse(
  string Id,
  string Name,
  string Description,
  string CreatorId,
  string Image,
  DateTime CreatedDateTime,
  DateTime UpdatedDateTime);