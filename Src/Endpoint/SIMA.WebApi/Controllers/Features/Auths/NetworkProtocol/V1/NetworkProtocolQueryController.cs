using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.MainAggregates;
using SIMA.Application.Query.Contract.Features.Auths.NetworkProtocols;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.NetworkProtocol.V1;

[Route("Basic/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "NetworkProtocol")]
public class NetworkProtocolQueryController: ControllerBase
{
    private readonly IMediator _mediator;

    public NetworkProtocolQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get()
    {
        var query = new GetAllNetworlProtocolQuery() {  };
        var result = await _mediator.Send(query);
        return result;
    }
}
