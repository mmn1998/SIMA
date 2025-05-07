using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Helpers;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper.FileHelper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Features.Auths.Helpers;

public class HelpDownloadQueryHandler : IQueryHandler<GetHelpDocumentQuery, Result<GetHelpDocumentQueryResult>>,
    IQueryHandler<GetHelpVideoQuery, FileStream>
{
    private readonly IFileService _fileService;
    private readonly IWebHostEnvironment _webHost;

    public HelpDownloadQueryHandler(IFileService fileService, IWebHostEnvironment webHost)
    {
        _fileService = fileService;
        _webHost = webHost;
    }
    public async Task<Result<GetHelpDocumentQueryResult>> Handle(GetHelpDocumentQuery request, CancellationToken cancellationToken)
    {
        var rootPath = _webHost.ContentRootPath;
        var filePath = Path.Combine(rootPath, "wwwroot", "Help", "help.pdf");
        var fileContent = await _fileService.Download(filePath) ?? throw SimaResultException.AccessDeny;
        var response = new GetHelpDocumentQueryResult
        {
            ContentType = "application/pdf",
            Extension = "pdf",
            FileContent = fileContent,
            Name = "راهنمای کاربری تبادلات ارزی (بخش اول از فاز یک).pdf"
        };
        return Result.Ok(response);
    }

    public async Task<FileStream> Handle(GetHelpVideoQuery request, CancellationToken cancellationToken)
    {
        var rootPath = _webHost.ContentRootPath;
        var filePath = Path.Combine(rootPath, "wwwroot", "Help", "TrustyDraftFinalVideo.mp4");
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return fileStream;           
    }
}
