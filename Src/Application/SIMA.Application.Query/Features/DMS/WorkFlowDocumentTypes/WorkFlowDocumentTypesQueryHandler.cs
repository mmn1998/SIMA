using SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.DMS.WorkFlowDocumentTypes;

namespace SIMA.Application.Query.Features.DMS.WorkFlowDocumentTypes;

public class WorkFlowDocumentTypesQueryHandler : IQueryHandler<GetAllWorkFlowDocumentTypesQuery, Result<List<GetWorkFlowDocumentTypeQueryResult>>>,
    IQueryHandler<GetWorkFlowDocumentTypeQuery, Result<GetWorkFlowDocumentTypeQueryResult>>
{
    private readonly IWorkFlowDocumentTypeQueryRepository _repository;

    public WorkFlowDocumentTypesQueryHandler(IWorkFlowDocumentTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<GetWorkFlowDocumentTypeQueryResult>>> Handle(GetAllWorkFlowDocumentTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request.Request);
    }

    public async Task<Result<GetWorkFlowDocumentTypeQueryResult>> Handle(GetWorkFlowDocumentTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}
