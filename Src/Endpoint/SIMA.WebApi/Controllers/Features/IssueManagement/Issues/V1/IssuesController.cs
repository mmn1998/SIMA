using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.IssueManagement.Issues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SIMA.WebApi.Controllers.Features.IssueManagement.Issues.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Issues")]
//[Authorize]
public class IssuesController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssuesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.IssuePost)]
    public async Task<Result> Post([FromBody] CreateIssueCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.IssuePut)]
    public async Task<Result> Put([FromBody] ModifyIssueCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.IssueDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteIssueCommand { Id = id };
        return await _mediator.Send(command);
    }
    [HttpPost("IssueComment")]
    [SimaAuthorize(Permissions.IssueCommentsPost)]
    public async Task<Result> Post([FromBody] CreateIssueCommentCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("IssueComment")]
    [SimaAuthorize(Permissions.IssueCommentsDelete)]
    public async Task<Result> Delete([FromQuery] long id, [FromQuery] long issueId)
    {
        var command = new DeleteIssueCommentCommand
        {
            Id = id,
            IssueId = issueId
        };
        return await _mediator.Send(command);
    }

    [HttpPost("IssueRunAction")]
    //[SimaAuthorize(Permissions.IssueRunActionPost)]
    public async Task<Result> IssueRunAction([FromBody] IssueRunActionCommand command)
    {
        return await _mediator.Send(command);
    }

}
