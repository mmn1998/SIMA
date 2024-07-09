using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.WebApi.Extensions;

namespace SIMA.WebApi.Controllers.Features.IssueManagement.Issues.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Issues")]
[Authorize]
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
    [HttpPost("myIssueList")]
    [SimaAuthorize(Permissions.MyIssueList)]
    public async Task<Result> MyIssueList(GetMyIssueListQuery request)
    {
        return await _mediator.Send(request);
    }
    /// <summary>
    /// فهرست حوزه من
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.IssueGetAll)]
    public async Task<Result> Get(GetAllIssuesQuery request)
    {
        return await _mediator.Send(request);
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.IssueGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetIssuesQuery { Id = id };
        var result = await _mediator.Send(query);
        foreach (var document in result.Data.IssueDocuments)
        {
            document.DocumentContentType = document.DocumentExtentionName.GetContentType();
        }
        return result;
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
