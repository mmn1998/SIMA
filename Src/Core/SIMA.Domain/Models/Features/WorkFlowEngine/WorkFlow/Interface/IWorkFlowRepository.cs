using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface
{
    public interface IWorkFlowRepository : IRepository<Entities.WorkFlow>
    {
        Task<Entities.WorkFlow> GetById(long id);
        Task<Entities.WorkFlow> GetById2(WorkFlowId workFlowId);
        Task<Step> GetStepById(long id);
        Task<Entities.WorkFlow> GetWorkFlowByDomainId(long domainId);
        Task<GetWorkflowInfoByIdResponseQueryResult> GetWorkflowInfoById(long workFlowId);
        Task<GetWorkflowInfoByIdResponseQueryResult> GetNextStepById(long workFlowId, long targetId , long progressId);

    }
}
