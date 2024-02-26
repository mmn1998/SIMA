using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.State;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.Step;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlow;

public interface IWorkFlowQueryRepository : IQueryRepository
{
    Task<GetWorkFlowQueryResult> FindById(long id);
    Task<List<GetWorkFlowQueryResult>> GetAll();
    Task<List<GetStepQueryResult>> GetAllStep();
    Task<List<GetStepQueryResult>> GetAllStepByWorkFlowId(long id);
    Task<GetStepQueryResult> GetStepById(long Id);
    Task<GetStateQueryResult> GetStateById(long stateId);
    Task<List<GetStateQueryResult>> GetAllStates();
    Task<List<GetWorkFlowQueryResult>> GetByProjectId(long projectId);
    Task<List<GetStateQueryResult>> GetAllStatesByWorkFlowId(long id);
}
