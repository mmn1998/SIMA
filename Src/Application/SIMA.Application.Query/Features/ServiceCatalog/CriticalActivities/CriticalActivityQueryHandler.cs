using SIMA.Application.Query.Contract.Features.ServiceCatalog.CriticalActivities;
using SIMA.Application.Query.Services.SimaReposrtServices;
using SIMA.Framework.Common.Helper.ExportHelpers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.CriticalActivities;

namespace SIMA.Application.Query.Features.ServiceCatalog.CriticalActivities;

public class CriticalActivityQueryHandler : IQueryHandler<GetCriticalActivityQuery, Result<GetCriticalActivityQueryResult>>,
    IQueryHandler<GetAllCriticalActivitiesQuery, Result<IEnumerable<GetAllCriticalActivitiesQueryResult>>>
{
    private readonly ICriticalActivitiyQueryRepository _repository;
    private readonly ISimaReportService _reportService;

    public CriticalActivityQueryHandler(ICriticalActivitiyQueryRepository repository, ISimaReportService reportService)
    {
        _repository = repository;
        _reportService = reportService;
    }
    public async Task<Result<GetCriticalActivityQueryResult>> Handle(GetCriticalActivityQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetDetail(id: request.Id, issueId: request.IssueId);
    }

    public async Task<Result<IEnumerable<GetAllCriticalActivitiesQueryResult>>> Handle(GetAllCriticalActivitiesQuery request, CancellationToken cancellationToken)
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
}
