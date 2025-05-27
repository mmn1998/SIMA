using SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityPlans;
using SIMA.Application.Query.Services.SimaReposrtServices;
using SIMA.Framework.Common.Helper.ExportHelpers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.BCP.BusinessContinuityPlans;

namespace SIMA.Application.Query.Features.BCP.BusinessContinuityPlans;

public class BusinessContinuityPlanQueryHandler : IQueryHandler<GetBusinessContinuityPlanQuery, Result<GetBusinessContinuityPlanQueryResult>>,
IQueryHandler<GetAllBusinessContinuityPlansQuery, Result<IEnumerable<GetAllBusinessContinuityPlansQueryResult>>>,
IQueryHandler<GetAllPlanVersioningsByPlanIdQuery, Result<IEnumerable<GetAllPlanVersioningsByPlanIdQueryResult>>>,
IQueryHandler<GetAllPlanAssumptionsByPlanIdQuery, Result<IEnumerable<GetAllPlanVersioningsByPlanIdQueryResult>>>,
IQueryHandler<GetBusinessContinuityPlanByVersionQuery,Result<GetBusinessContinuityPlanQueryResult>>
{
    private readonly IBusinessContinuityPlanQueryRepository _repository;
    private readonly ISimaReportService _reportService;

    public BusinessContinuityPlanQueryHandler(IBusinessContinuityPlanQueryRepository repository, ISimaReportService reportService)
    {
        _repository = repository;
        _reportService = reportService;
    }
    public async Task<Result<GetBusinessContinuityPlanQueryResult>> Handle(GetBusinessContinuityPlanQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    // throw new SimaResultException(CodeMessges._400Code, Messages.NotFound);
    
    public async Task<Result<IEnumerable<GetAllBusinessContinuityPlansQueryResult>>> Handle(GetAllBusinessContinuityPlansQuery request, CancellationToken cancellationToken)
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

    public async Task<Result<IEnumerable<GetAllPlanVersioningsByPlanIdQueryResult>>> Handle(GetAllPlanVersioningsByPlanIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPlanVersioningByPlanId(request.BusinessContinuityPlanId);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAllPlanVersioningsByPlanIdQueryResult>>> Handle(GetAllPlanAssumptionsByPlanIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPlanAssumptionByPlanId(request.BusinessContinuityPlanId);
        return Result.Ok(result);
    }

    public async Task<Result<GetBusinessContinuityPlanQueryResult>> Handle(GetBusinessContinuityPlanByVersionQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAndVersionNumber(request);
        return Result.Ok(result);
    }
}
