using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Features.IssueManagement.Issues.Mappers;
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
    IQueryHandler<GetCasesByWorkflowIdQuery, Result<List<GetCasesByWorkflowIdQueryResult>>>
{
    private readonly IIssueQueryRepository _repository;

    public IssueQueryHandler(IIssueQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<GetAllIssueQueryResult>>> Handle(GetMyIssueListQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllMyIssue(request);
    }
    public async Task<Result<IEnumerable<GetAllIssueQueryResult>>> Handle(GetAllIssuesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
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
}
