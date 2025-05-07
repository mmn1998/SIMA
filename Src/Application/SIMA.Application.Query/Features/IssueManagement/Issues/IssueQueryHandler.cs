using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Features.IssueManagement.Issues.Mappers;
using SIMA.Application.Query.Services.SimaReposrtServices;
using SIMA.Framework.Common.Helper.ExportHelpers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.IssueManagement.Issues;

namespace SIMA.Application.Query.Features.IssueManagement.Issues;

public class IssueQueryHandler :
    IQueryHandler<GetAllIssuesQuery, Result<IEnumerable<GetAllIssueQueryResult>>>,
    IQueryHandler<GetMyIssueListQuery, Result<IEnumerable<GetAllIssueQueryResult>>>,
    IQueryHandler<GetIssuesQuery, Result<GetIssueQueryResult>>,
    IQueryHandler<GetIssueHistoriesByIdQuery, Result<GetIssueHistoriesByIdQueryResult>>,
    IQueryHandler<GetIssueHistoriesByIssueIdQuery, Result<IEnumerable<GetIssueHistoriesByIssueIdQueryResult>>>,
    IQueryHandler<GetCasesByWorkflowIdQuery, Result<List<GetCasesByWorkflowIdQueryResult>>>,
    IQueryHandler<GetIssueComponentQuery, Result<GetIssueComponentQueryResult>>
{
    private readonly IIssueQueryRepository _repository;
    private readonly ISimaReportService _reportService;

    public IssueQueryHandler(IIssueQueryRepository repository, ISimaReportService reportService)
    {
        _repository = repository;
        _reportService = reportService;
    }
    public async Task<Result<IEnumerable<GetAllIssueQueryResult>>> Handle(GetMyIssueListQuery request, CancellationToken cancellationToken)
    {
        var res = await _repository.GetAllMyIssue(request);
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
    public async Task<Result<IEnumerable<GetAllIssueQueryResult>>> Handle(GetAllIssuesQuery request, CancellationToken cancellationToken)
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

    public async Task<Result<GetIssueQueryResult>> Handle(GetIssuesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        result.WorkFlowFileContent = result.WorkFlowFileContent.ColorizeCurrentStep(result.BpmnId);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetIssueHistoriesByIssueIdQueryResult>>> Handle(GetIssueHistoriesByIssueIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetIssueHistoryByIssueId(request.IssueId);
        return Result.Ok(result);
    }

    public async Task<Result<GetIssueHistoriesByIdQueryResult>> Handle(GetIssueHistoriesByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetIssueHistoryById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetCasesByWorkflowIdQueryResult>>> Handle(GetCasesByWorkflowIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.GetCasesByWorkflowId(request.WorkFlowId);
            return Result.Ok(result);

        }
        catch (Exception e)
        {

            throw;
        }
    }

    public async Task<Result<GetIssueComponentQueryResult>> Handle(GetIssueComponentQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.ComponentIssue(request.SourceId, request.IssueId);
        if (result.IssueInfo is not null)
            result.IssueInfo.WorkFlowFileContent =
                result.IssueInfo.WorkFlowFileContent.ColorizeCurrentStep(result.IssueInfo.CurrentStepBpmnId);
        return Result.Ok(result);
    }
}
