using SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;
using SIMA.Application.Query.Services.SimaReposrtServices;
using SIMA.Framework.Common.Helper.ExportHelpers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Services;

namespace SIMA.Application.Query.Features.ServiceCatalog.Services;

public class ServiceQueryHandler : IQueryHandler<GetServiceQuery, Result<GetServiceQueryResult>>,
    IQueryHandler<GetAllServicesQuery, Result<IEnumerable<GetAllServicesQueryResult>>>,
    IQueryHandler<GetServiceByCodeQuery, Result<GetServiceQueryResult>>
{
    private readonly IServiceQueryRepository _repository;
    private readonly ISimaReportService _reportService;

    public ServiceQueryHandler(IServiceQueryRepository repository, ISimaReportService reportService)
    {
        _repository = repository;
        _reportService = reportService;
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
            var excelByte = _reportService.ExportToExcel(res.Data);
            res.Data = res.Data.Skip(request.Page).Take(request.PageSize);
            var exportResult = new ExportResult
            {
                Name = _reportService.GenerateFileName(this.GetType().Name.Replace("QueryHandler", "")) + "." + ExportExtensions.ExcelExtension,
                ContentType = ExportContentTypes.ExcelContentType,
                Extension = ExportExtensions.ExcelExtension,
                FileContent = excelByte,
            };
            res.ExportResult = exportResult;
        }
        
        return res;
    }

    public async Task<Result<GetServiceQueryResult>> Handle(GetServiceByCodeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByCode(code: request.Code, issueId: request.IssueId);
        return Result.Ok(result);
    }
}
