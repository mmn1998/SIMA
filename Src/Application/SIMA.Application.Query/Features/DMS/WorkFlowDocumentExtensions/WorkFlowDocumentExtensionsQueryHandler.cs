using SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentExtensions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.DMS.WorkFlowDocumentExtensions;

namespace SIMA.Application.Query.Features.DMS.WorkFlowDocumentExtensions
{
    public class WorkFlowDocumentExtensionsQueryHandler : IQueryHandler<GetAllWorkFlowDocumentExtensionQuery, Result<IEnumerable<GetWorkFlowDocumentExtensionQueryResult>>>,
    IQueryHandler<GetWorkFlowDocumentExtensionQuery, Result<GetWorkFlowDocumentExtensionQueryResult>>
    {
        private readonly IWorkFlowDocumentExtensionQueryRepository _repository;

        public WorkFlowDocumentExtensionsQueryHandler(IWorkFlowDocumentExtensionQueryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<IEnumerable<GetWorkFlowDocumentExtensionQueryResult>>> Handle(GetAllWorkFlowDocumentExtensionQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request);
        }

        public async Task<Result<GetWorkFlowDocumentExtensionQueryResult>> Handle(GetWorkFlowDocumentExtensionQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return Result.Ok(result);
        }
    }
}
