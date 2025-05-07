using AutoMapper;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor;

namespace SIMA.Application.Query.Features.WorkFlowEngine.WorkFlowActor.Mapper
{
    public class WorkFlowActorMapper : Profile
    {
        public WorkFlowActorMapper()
        {
            CreateMap<Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites.WorkFlowActor, GetWorkFlowActorQueryResult>();
        }
    }
}
