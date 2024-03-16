using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.State;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.Step;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlow;

namespace SIMA.Application.Query.Features.WorkFlowEngine.WorkFlow;

public class WorkFlowQueryHandler : IQueryHandler<GetAllWorkFlowsQuery, Result<List<GetWorkFlowQueryResult>>>, IQueryHandler<GetWorkFlowQuery, Result<GetWorkFlowQueryResult>>,
    IQueryHandler<GetAllStepsQuery, Result<IEnumerable<GetStepQueryResult>>>, IQueryHandler<GetStepQuery, Result<GetStepQueryResult>>,
    IQueryHandler<GetAllStatesQuery, Result<IEnumerable<GetStateQueryResult>>>
    , IQueryHandler<GetStateQuery, Result<GetStateQueryResult>>
    , IQueryHandler<GetWorkFlowByProjectQuery, Result<List<GetWorkFlowQueryResult>>>
    , IQueryHandler<GetStatesByWorkFlowQuery, Result<List<GetStateQueryResult>>>
     , IQueryHandler<GetStepsByWorkFlowQuery, Result<List<GetStepQueryResult>>>
{
    private readonly IMapper _mapper;
    private readonly IWorkFlowQueryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public WorkFlowQueryHandler(IMapper mapper, IWorkFlowQueryRepository repository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<GetWorkFlowQueryResult>> Handle(GetWorkFlowQuery request, CancellationToken cancellationToken)
    {
        var actor = await _repository.FindById(request.Id);
        return Result.Ok(actor);
    }
    public async Task<Result<List<GetWorkFlowQueryResult>>> Handle(GetAllWorkFlowsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
    public async Task<Result<GetStepQueryResult>> Handle(GetStepQuery request, CancellationToken cancellationToken)
    {
        var step = await _repository.GetStepById(request.Id);
        return Result.Ok(step);
    }
    public async Task<Result<IEnumerable<GetStepQueryResult>>> Handle(GetAllStepsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllStep(request);

    }
    public async Task<Result<GetStateQueryResult>> Handle(GetStateQuery request, CancellationToken cancellationToken)
    {
        var state = await _repository.GetStateById(request.Id);
        return Result.Ok(state);
    }
    public async Task<Result<IEnumerable<GetStateQueryResult>>> Handle(GetAllStatesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllStates(request);
    }
    public async Task<Result<List<GetWorkFlowQueryResult>>> Handle(GetWorkFlowByProjectQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByProjectId(request.ProjectId);
        return Result.Ok(entity);
    }

    public async Task<Result<List<GetStateQueryResult>>> Handle(GetStatesByWorkFlowQuery request, CancellationToken cancellationToken)
    {
        var res = await _repository.GetAllStatesByWorkFlowId(request.Id);
        return Result.Ok(res);
    }

    public async Task<Result<List<GetStepQueryResult>>> Handle(GetStepsByWorkFlowQuery request, CancellationToken cancellationToken)
    {
        var entites = await _repository.GetAllStepByWorkFlowId(request.Id);
        return Result.Ok(entites);
    }
}
