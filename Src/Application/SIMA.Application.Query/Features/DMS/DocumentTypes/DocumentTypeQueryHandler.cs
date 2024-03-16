using SIMA.Application.Query.Contract.Features.DMS.DocumentTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.DMS.DocumentTypes;

namespace SIMA.Application.Query.Features.DMS.DocumentTypes;

public class DocumentTypeQueryHandler : IQueryHandler<GetAllDocumentTypesQuery, Result<List<GetDocumentTypeQueryResult>>>,
    IQueryHandler<GetDocumentTypeQuery, Result<GetDocumentTypeQueryResult>>
{
    private readonly IDocumentTypeQueryRepository _repository;

    public DocumentTypeQueryHandler(IDocumentTypeQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<GetDocumentTypeQueryResult>>> Handle(GetAllDocumentTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetDocumentTypeQueryResult>> Handle(GetDocumentTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}
