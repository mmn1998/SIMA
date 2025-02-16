using SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;
using SIMA.Application.Query.Services.SimaReposrtServices;
using SIMA.Framework.Common.Helper.ExportHelpers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Services;
using System.Reflection;

namespace SIMA.Application.Query.Features.ServiceCatalog.Services;

public class ServiceQueryHandler : IQueryHandler<GetServiceQuery, Result<GetServiceQueryResult>>,
    IQueryHandler<GetAllServicesQuery, Result<IEnumerable<GetAllServicesQueryResult>>>
{
    private readonly IServiceQueryRepository _repository;
    private readonly ISimaReposrtService _reposrtService;

    public ServiceQueryHandler(IServiceQueryRepository repository, ISimaReposrtService reposrtService)
    {
        _repository = repository;
        _reposrtService = reposrtService;
    }
    public async Task<Result<GetServiceQueryResult>> Handle(GetServiceQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(id: request.Id, issueId: request.IssueId);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAllServicesQueryResult>>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
    {
        var res = await _repository.GetAll(request);
        if (!string.IsNullOrEmpty(request.FormatType))
        {
            var excelByte = _reposrtService.ExportToExcel(res.Data);
            var exportResult = new ExportResult
            {
                Name = _reposrtService.GenerateFileName(this.GetType().Name.Replace("QueryHandler", "")) + "." + ExportExtensions.ExcelExtension,
                ContentType = ExportContentTypes.ExcelContentType,
                Extension = ExportExtensions.ExcelExtension,
                FileContent = excelByte,
            };
            res.ExportResult = exportResult;
        }
        return res;
    }
}
