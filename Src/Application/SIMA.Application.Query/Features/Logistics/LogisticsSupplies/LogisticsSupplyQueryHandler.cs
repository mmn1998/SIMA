using SIMA.Application.Query.Contract.Features.Logistics.LogisticsSupplies;
using SIMA.Application.Query.Services.SimaReposrtServices;
using SIMA.Framework.Common.Helper.ExportHelpers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Logistics.LogisticsSupplies;

namespace SIMA.Application.Query.Features.Logistics.LogisticsSupplies;

public class LogisticsSupplyQueryHandler : IQueryHandler<GetAllLogisticsSuppliesQuery, Result<IEnumerable<GetLogisticsSupplyQueryResult>>>,
    IQueryHandler<GetAllMyLogisticsSuppliesQuery, Result<IEnumerable<GetLogisticsSupplyQueryResult>>>,
    IQueryHandler<GetLogisticsSupplyQuery, Result<GetLogisticsSupplyDeatilQueryResult>>,
    IQueryHandler<GetLogisticsSupplyGoodsCategoryQuery, Result<IEnumerable<GetLogisticsSupplyGoodsCategoryQueryResult>>>,
    IQueryHandler<GetAllOrderingByLogisticsSupplyIdQuery, Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>>,
    IQueryHandler<GetAllPaymentCommandByLogisticsSupplyIdQuery, Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>>,
    IQueryHandler<GetPrePaymentCommandByLogisticsSupplyQuery, Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>>
{
    private readonly ILogisticsSupplyQueryRepository _repository;
    private readonly ISimaReportService _reportService;

    public LogisticsSupplyQueryHandler(ILogisticsSupplyQueryRepository repository, ISimaReportService reportService)
    {
        _repository = repository;
        _reportService = reportService;
    }
    public async Task<Result<GetLogisticsSupplyDeatilQueryResult>> Handle(GetLogisticsSupplyQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetDetail(id: request.Id, logisticsRequestId: request.LogisticsRequestId);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetLogisticsSupplyQueryResult>>> Handle(GetAllLogisticsSuppliesQuery request, CancellationToken cancellationToken)
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

    public async Task<Result<IEnumerable<GetLogisticsSupplyQueryResult>>> Handle(GetAllMyLogisticsSuppliesQuery request, CancellationToken cancellationToken)
    {
        var res = await _repository.GetAllMy(request);
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

    public async Task<Result<IEnumerable<GetLogisticsSupplyGoodsCategoryQueryResult>>> Handle(GetLogisticsSupplyGoodsCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetGoodsCategoryBySupplyId(request.LogisticsSupplyId);
    }

    public async Task<Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>> Handle(GetAllOrderingByLogisticsSupplyIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetOrderingByLogisticsSupplyId(request.LogisticsSupplyId);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>> Handle(GetAllPaymentCommandByLogisticsSupplyIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPaymentCommandByLogisticsSupplyId(request.LogisticsSupplyId);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetOrderingByLogisticsSupplyIdQueryResult>>> Handle(GetPrePaymentCommandByLogisticsSupplyQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPrePaymentCommandByLogisticsSupplyId(request.LogisticsSupplyId);
        return Result.Ok(result);
    }
}
