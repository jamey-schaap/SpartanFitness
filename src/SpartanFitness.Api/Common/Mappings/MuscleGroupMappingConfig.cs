using Mapster;

using SpartanFitness.Application.MuscleGroups.Commands.CreateMuscleGroup;
using SpartanFitness.Application.MuscleGroups.Queries.GetMuscleGroupPage;
using SpartanFitness.Contracts.Common;
using SpartanFitness.Contracts.MuscleGroups;
using SpartanFitness.Domain.Aggregates;
using SpartanFitness.Domain.Common.Models;

namespace SpartanFitness.Api.Common.Mappings;

public class MuscleGroupMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<(CreateMuscleGroupRequest Request, string UserId), CreateMuscleGroupCommand>()
      .Map(dest => dest.UserId, src => src.UserId)
      .Map(dest => dest, src => src.Request);

    config.NewConfig<MuscleGroup, MuscleGroupResponse>()
      .Map(dest => dest.Id, src => src.Id.Value)
      .Map(dest => dest.CoachId, src => src.CreatorId.Value)
      .Map(dest => dest, src => src);

    config.NewConfig<PagingRequest, GetMuscleGroupPageQuery>()
      .Map(dest => dest.PageNumber, src => src.Page)
      .Map(dest => dest.PageSize, src => src.Size)
      .Map(dest => dest.SearchQuery, src => src.Query)
      .Map(dest => dest, src => src);

    config.NewConfig<Page<MuscleGroup>, MuscleGroupPageResponse>()
      .Map(dest => dest.MuscleGroups, src => src.Content)
      .Map(dest => dest, src => src);
    
    config.NewConfig<MuscleGroup, MuscleGroupPageMusclesResponse>()
      .Map(dest => dest.Id, src => src.Id.Value)
      .Map(dest => dest.CreatorId, src => src.CreatorId.Value)
      .Map(dest => dest, src => src);
  }
}