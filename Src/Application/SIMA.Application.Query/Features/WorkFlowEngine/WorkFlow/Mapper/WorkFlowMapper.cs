using AutoMapper;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.State;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.Step;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;


namespace SIMA.Application.Query.Features.WorkFlowEngine.WorkFlow.Mapper
{
    public class WorkFlowMapper : Profile
    {
        public WorkFlowMapper()
        {
            CreateMap<Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities.WorkFlow, GetWorkFlowQueryResult>();

            CreateMap<Step, GetStepQueryResult>();

            CreateMap<State, GetStateQueryResult>();

        }
    }
}
