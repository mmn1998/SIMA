using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.Forms;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.Forms.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Forms")]
[Authorize]
public class FormsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FormsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateFormCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyFormCommand command)
    {
        return await _mediator.Send(command);
    }
}
