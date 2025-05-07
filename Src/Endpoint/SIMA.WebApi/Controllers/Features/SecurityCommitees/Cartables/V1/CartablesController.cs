using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.SecurityCommitees.Cartables;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.SecurityCommitees.Cartables.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Cartables")]
public class CartablesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartablesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CartableCommand command)
    {
        return await _mediator.Send(command);
    }
}
