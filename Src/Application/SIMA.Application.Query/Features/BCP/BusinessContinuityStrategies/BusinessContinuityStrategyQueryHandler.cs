using SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityStrategies;
using SIMA.Application.Query.Services.SimaReposrtServices;
using SIMA.Framework.Common.Helper.ExportHelpers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.BusinessContinuityStrategies;

namespace SIMA.Application.Query.Features.BCP.BusinessContinuityStrategies;

public class BusinessContinuityStrategyQueryHandler : IQueryHandler<GetBusinessContinuityStrategyQuery, Result<GetBusinessContinuityStrategyQueryResult>>,
    IQueryHandler<GetAllBusinessContinuityStrategiesQuery, Result<IEnumerable<GetAllBusinessContinuityStrategiesQueryResult>>>
{
    private readonly IBusinessContinuityStrategyQueryRepository _repository;
    private readonly ISimaReportService _reportService;

    public BusinessContinuityStrategyQueryHandler(IBusinessContinuityStrategyQueryRepository repository, ISimaReportService reportService)
    {
        _repository = repository;
        _reportService = reportService;
    }
    public async Task<Result<GetBusinessContinuityStrategyQueryResult>> Handle(GetBusinessContinuityStrategyQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAllBusinessContinuityStrategiesQueryResult>>> Handle(GetAllBusinessContinuityStrategiesQuery request, CancellationToken cancellationToken)
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