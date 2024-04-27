using MediatR;
using Microsoft.AspNetCore.Mvc;

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
}
