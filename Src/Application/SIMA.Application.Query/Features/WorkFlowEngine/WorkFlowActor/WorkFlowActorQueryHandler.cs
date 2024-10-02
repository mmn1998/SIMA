using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlowActor;

namespace SIMA.Application.Query.Features.WorkFlowEngine.WorkFlowActor;

public class WorkFlowActorQueryHandler : IQueryHandler<GetWorkFlowActorQuery, Result<GetWorkFlowActorQueryResult>>,
    IQueryHandler<GetAllWorkFlowActorsQuery, Result<IEnumerable<GetWorkFlowActorQueryResult>>>,
    IQueryHandler<GetWorkflowActorEmployeeQuery, Result<IEnumerable<GetWorkflowActorEmployeeQueryResult>>>
{
    private readonly IMapper _mapper;
    private readonly IWorkFlowActorQueryRepository _repository;

    public WorkFlowActorQueryHandler(IMapper mapper, IWorkFlowActorQueryRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Result<GetWorkFlowActorQueryResult>> Handle(GetWorkFlowActorQuery request, CancellationToken cancellationToken)
    {
        var actor = await _repository.FindById(request.Id);
        var result = _mapper.Map<GetWorkFlowActorQueryResult>(actor);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetWorkFlowActorQueryResult>>> Handle(GetAllWorkFlowActorsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<IEnumerable<GetWorkflowActorEmployeeQueryResult>>> Handle(GetWorkflowActorEmployeeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetEmployee(request.Id);
        return Result.Ok(result);
    }
}
