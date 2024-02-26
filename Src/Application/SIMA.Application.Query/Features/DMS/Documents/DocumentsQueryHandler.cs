using SIMA.Application.Query.Contract.Features.DMS.Documents;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper.FileHelper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.DMS.Documents;

namespace SIMA.Application.Query.Features.DMS.Documents;

public class DocumentsQueryHandler : IQueryHandler<GetDownloadDocumentQuery, GetDownloadDocumentQueryResult>, IQueryHandler<GetAllDocumentsQuery, Result<List<GetAllDocumentQueryResult>>>
{
    private readonly IDocumentQueryRepository _repository;
    private readonly IFileService _fileService;

    public DocumentsQueryHandler(IDocumentQueryRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }
    public async Task<GetDownloadDocumentQueryResult> Handle(GetDownloadDocumentQuery request, CancellationToken cancellationToken)
    {
        var documentResult = await _repository.GetForDownload(request.DocumetId);
        var response = new GetDownloadDocumentQueryResult();
        var fileContent = await _fileService.Download(documentResult.FileAddress);
        if (fileContent == null)
        {
            throw new SimaResultException("404", "file not found");
        }
        response.FileContent = fileContent;
        response.Extension = documentResult.Extension;        
        return response;
    }

    public async Task<Result<List<GetAllDocumentQueryResult>>> Handle(GetAllDocumentsQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll(request.Request);
        return Result.Ok(result);
    }
}
