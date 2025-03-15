using SIMA.Application.Query.Contract.Features.RiskManagement.Risks;
using SIMA.Application.Query.Services.SimaReposrtServices;
using SIMA.Framework.Common.Helper.ExportHelpers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.RiskManagement.Risks;

namespace SIMA.Application.Query.Features.RiskManagement.Risks;

public class RiskQueryHandler : IQueryHandler<GetAllRisksQuery, Result<IEnumerable<GetAllRisksQueryResult>>>,
    IQueryHandler<GetRiskQuery, Result<GetRiskQueryResult>>

{
    private readonly IRiskQueryRepository _repository;
    private readonly ISimaReportService _reportService;

    public RiskQueryHandler(IRiskQueryRepository repository, ISimaReportService reportService)
    {
        _repository = repository;
        _reportService = reportService;
    }
    public async Task<Result<GetRiskQueryResult>> Handle(GetRiskQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAllRisksQueryResult>>> Handle(GetAllRisksQuery request, CancellationToken cancellationToken)
    {
        var res = await _repository.GetAll(request);
        if (!string.IsNullOrEmpty(request.FormatType))
        {
            res.Data = res.Data.Skip(request.Page).Take(request.PageSize);
            var excelByte = _reportService.ExportToExcel(res.Data);
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
