using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.IssueManagement.IssueTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.IssueManagement.IssueTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "IssueTypes")]
public class IssueTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssueTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.IssueTypesPost)]
    public async Task<Result> Post([FromBody] CreateIssueTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.IssueTypesPut)]
    public async Task<Result> Put([FromBody] ModifyIssueTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.IssueTypesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteIssueTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}
