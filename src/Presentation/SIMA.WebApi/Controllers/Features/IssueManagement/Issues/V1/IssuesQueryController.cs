using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.IssueManagement.Issues.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Issues")]
public class IssuesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssuesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// سرویس دریافت فهرست موارد ثبت شده توسط کاربر جاری
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("myIssueList")]
    [SimaAuthorize(Permissions.MyIssueList)]
    public async Task<Result> MyIssueList([FromQuery] GetMyIssueListQuery request)
    {
        return await _mediator.Send(request);
    }
    /// <summary>
    /// فهرست حوزه من
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [SimaAuthorize(Permissions.IssueGetAll)]
    public async Task<Result> Get([FromQuery] GetAllIssuesQuery request)
    {
        return await _mediator.Send(request);
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.IssueGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetIssuesQuery { Id = id };
        return await _mediator.Send(query);
    }

    [HttpGet("History/{issueId}")]
    [SimaAuthorize(Permissions.GetHistory)]
    public async Task<Result> GetHistory([FromRoute] long issueId)
    {
        var query = new GetIssueHistoriesByIssueIdQuery { IssueId = issueId };
        return await _mediator.Send(query);
    }

    [HttpGet("HistoryById/{id}")]
    [SimaAuthorize(Permissions.GetHistoryById)]
    public async Task<Result> GetHistoryById([FromRoute] long id)
    {
        var query = new GetIssueHistoriesByIdQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpGet("CasesByWorkflowId/{workflowId}")]
    [SimaAuthorize(Permissions.IssueGet)]
    public async Task<Result> GetCasesByWorkflowId([FromRoute] long workflowId)
    {
        var query = new GetCasesByWorkflowIdQuery { WorkFlowId = workflowId };
        return await _mediator.Send(query);
    }
}
