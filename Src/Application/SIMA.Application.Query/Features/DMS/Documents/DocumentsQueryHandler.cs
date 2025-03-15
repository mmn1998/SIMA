using Microsoft.AspNetCore.Hosting;
using SIMA.Application.Query.Contract.Features.DMS.Documents;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper.FileHelper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.DMS.Documents;
using SIMA.Resources;

namespace SIMA.Application.Query.Features.DMS.Documents;

public class DocumentsQueryHandler : IQueryHandler<GetDownloadDocumentQuery, GetDownloadDocumentQueryResult>, IQueryHandler<GetAllDocumentsQuery, Result<IEnumerable<GetAllDocumentQueryResult>>>
{
    private readonly IDocumentQueryRepository _repository;
    private readonly IFileService _fileService;
    private readonly IWebHostEnvironment _webHost;

    public DocumentsQueryHandler(IDocumentQueryRepository repository, IFileService fileService, IWebHostEnvironment webHost)
    {
        _repository = repository;
        _fileService = fileService;
        _webHost = webHost;
    }
    public async Task<GetDownloadDocumentQueryResult> Handle(GetDownloadDocumentQuery request, CancellationToken cancellationToken)
    {
        var response = new GetDownloadDocumentQueryResult();
        if (request.DocumetId == -1)
        {
            var rootPath = _webHost.ContentRootPath;
            var filePath = Path.Combine(rootPath, "wwwroot", "Help", "help.pdf");
            var fileContentHelp = await _fileService.Download(filePath) ?? throw SimaResultException.AccessDeny;
            response.ContentType = "application/pdf";
            response.Extension = "pdf";
            response.FileContent = fileContentHelp;
            response.Name = "راهنمای کاربری تبادلات ارزی (بخش اول از فاز یک).pdf";
            return response;
        }
        //else if (request.DocumetId == -2)
        //{
        //    var rootPath = _webHost.ContentRootPath;
        //    var filePath = Path.Combine(rootPath, "wwwroot", "Help", "help_2.pdf");
        //    var fileContentHelp = await _fileService.Download(filePath) ?? throw SimaResultException.AccessDeny;
        //    response.ContentType = "application/pdf";
        //    response.Extension = "pdf";
        //    response.FileContent = fileContentHelp;
        //    response.Name = "راهنمای کاربری تبادلات ارزی (بخش اول از فاز یک) (1).pdf";
        //    return response;
        //}
        var documentResult = await _repository.GetForDownload(request.DocumetId);
        var fileContent = await _fileService.Download(documentResult.FileAddress);
        if (fileContent == null)
        {
            throw new SimaResultException(CodeMessges._404Code, Messages.DownloadError);
        }
        response.FileContent = fileContent;
        response.Extension = documentResult.Extension;
        response.Name = documentResult.Name;
        return response;
    }

    public async Task<Result<IEnumerable<GetAllDocumentQueryResult>>> Handle(GetAllDocumentsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
