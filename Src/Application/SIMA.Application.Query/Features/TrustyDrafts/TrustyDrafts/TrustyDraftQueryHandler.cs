using SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts;
using SIMA.Application.Query.Services.SimaReposrtServices;
using SIMA.Framework.Common.Helper.ExportHelpers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.TrustyDrafts;

namespace SIMA.Application.Query.Features.TrustyDrafts.TrustyDrafts;

public class TrustyDraftQueryHandler : IQueryHandler<GetAllTrustyDraftsQuery, Result<IEnumerable<GetAllTrustyDraftsQueryResult>>>,
    IQueryHandler<GetAllMyTrustyDraftsQuery, Result<IEnumerable<GetAllTrustyDraftsQueryResult>>>,
    IQueryHandler<GetTrustyDraftQuery, Result<GetTrustyDraftQueryResult>>, IQueryHandler<GetAllTrustyDraftRequested, Result<IEnumerable<GetAllTrustyDraftRequestedResult>>>,
    IQueryHandler<GetAllDraftForPayment, Result<IEnumerable<GetAllDraftForPaymentResult>>>,
    IQueryHandler<GetAllReconcilliation, Result<IEnumerable<GetAllReconcilliationResult>>>,
    IQueryHandler<GetAllFrorEachDepartment, Result<IEnumerable<GetAllTrustyDraftRequestedResult>>>,
    IQueryHandler<GetAllTrustyDraftByBrokerQuery, Result<IEnumerable<GetAllTrustyDraftRequestedResult>>>
{
    private readonly ITrustyDraftQueryRepository _repository;
    private readonly ISimaReportService _simaReportService;

    public TrustyDraftQueryHandler(ITrustyDraftQueryRepository repository, ISimaReportService simaReportService)
    {
        _repository = repository;
        _simaReportService = simaReportService;
    }
    public async Task<Result<IEnumerable<GetAllTrustyDraftsQueryResult>>> Handle(GetAllTrustyDraftsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetTrustyDraftQueryResult>> Handle(GetTrustyDraftQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetAllTrustyDraftRequestedResult>>> Handle(GetAllTrustyDraftRequested request, CancellationToken cancellationToken)
    {
        var res = await _repository.GetAllRequested(request);
        if (!string.IsNullOrEmpty(request.FormatType))
        {
            var excelByte = _simaReportService.ExportToExcel(res.Data);
            res.Data = res.Data.Skip(request.Page).Take(request.PageSize);
            var exportResult = new ExportResult
            {
                Name = _simaReportService.GenerateFileName(this.GetType().Name.Replace("QueryHandler", "")) + "." + ExportExtensions.ExcelExtension,
                ContentType = ExportContentTypes.ExcelContentType,
                Extension = ExportExtensions.ExcelExtension,
                FileContent = excelByte,
            };
            res.ExportResult = exportResult;
        }
        return res;
    }

    public async Task<Result<IEnumerable<GetAllDraftForPaymentResult>>> Handle(GetAllDraftForPayment request, CancellationToken cancellationToken)
    {
        var res = await _repository.GetAllDraftForPayment(request);
        if (!string.IsNullOrEmpty(request.FormatType))
        {
            var excelByte = _simaReportService.ExportToExcel(res.Data);
            res.Data = res.Data.Skip(request.Page).Take(request.PageSize);
            var exportResult = new ExportResult
            {
                Name = _simaReportService.GenerateFileName(this.GetType().Name.Replace("QueryHandler", "")) + "." + ExportExtensions.ExcelExtension,
                ContentType = ExportContentTypes.ExcelContentType,
                Extension = ExportExtensions.ExcelExtension,
                FileContent = excelByte,
            };
            res.ExportResult = exportResult;
        }
        return res;
    }

    public async Task<Result<IEnumerable<GetAllReconcilliationResult>>> Handle(GetAllReconcilliation request, CancellationToken cancellationToken)
    {
        
        var res = await _repository.GetAllReconcilliation(request);
        if (!string.IsNullOrEmpty(request.FormatType))
        {
            var excelByte = _simaReportService.ExportToExcel(res.Data);
            res.Data = res.Data.Skip(request.Page).Take(request.PageSize);
            var exportResult = new ExportResult
            {
                Name = _simaReportService.GenerateFileName(this.GetType().Name.Replace("QueryHandler", "")) + "." + ExportExtensions.ExcelExtension,
                ContentType = ExportContentTypes.ExcelContentType,
                Extension = ExportExtensions.ExcelExtension,
                FileContent = excelByte,
            };
            res.ExportResult = exportResult;
        }
        return res;
    }

    public async Task<Result<IEnumerable<GetAllTrustyDraftRequestedResult>>> Handle(GetAllTrustyDraftByBrokerQuery request, CancellationToken cancellationToken)
    {
        if (request.BrokerId <= 0)
            request.BrokerId = null;
        return await _repository.GetAllByBrokerId(request);
    }

    public async Task<Result<IEnumerable<GetAllTrustyDraftRequestedResult>>> Handle(GetAllFrorEachDepartment request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllFrorEachDepartment(request);
    }

    public async Task<Result<IEnumerable<GetAllTrustyDraftsQueryResult>>> Handle(GetAllMyTrustyDraftsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllMy(request);
    }
}
