using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BranchManagement.Brokers;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.Brokers.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Brokers")]
[Authorize]
public class BrokersQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public BrokersQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get(GetAllBrokerQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetBrokerQuery { Id = id };
        return await _mediator.Send(query);
    }
}
