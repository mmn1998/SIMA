using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.CurrencyPaymentChannels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.CurrencyPaymentChannels.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/CurrencyPaymentChannels")]
public class CurrencyPaymentChannelsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrencyPaymentChannelsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.CurrencyPaymentChannelGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetCurrencyPaymentChannelQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.CurrencyPaymentChannelGetAll)]
    public async Task<Result> Get([FromBody] GetAllCurrencyPaymentChannelsQuery query)
    {
        return await _mediator.Send(query);
    }
}