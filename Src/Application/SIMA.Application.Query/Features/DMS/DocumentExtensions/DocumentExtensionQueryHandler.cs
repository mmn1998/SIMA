using MediatR;
using SIMA.Application.Query.Contract.Features.DMS.DocumentExtensions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.DMS.DocumentExtensions;

namespace SIMA.Application.Query.Features.DMS.DocumentExtensions;

public class DocumentExtensionQueryHandler : IQueryHandler<GetAllDocumentExtensionsQuery, Result<List<GetDocumentExtensionQueryResult>>>,
    IQueryHandler<GetDocumentExtensionQuery, Result<GetDocumentExtensionQueryResult>>
{
    private readonly IDocumentExtensionQueryRepository _repository;

    public DocumentExtensionQueryHandler(IDocumentExtensionQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<GetDocumentExtensionQueryResult>>> Handle(GetAllDocumentExtensionsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetDocumentExtensionQueryResult>> Handle(GetDocumentExtensionQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}
