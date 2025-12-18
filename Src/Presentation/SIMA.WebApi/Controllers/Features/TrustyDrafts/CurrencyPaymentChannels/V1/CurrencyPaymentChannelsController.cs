using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.CurrencyPaymentChannels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.CurrencyPaymentChannels.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/CurrencyPaymentChannels")]
public class CurrencyPaymentChannelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrencyPaymentChannelsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.CurrencyPaymentChannelPost)]
    public async Task<Result> Post([FromBody] CreateCurrencyPaymentChannelCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.CurrencyPaymentChannelPut)]
    public async Task<Result> Put([FromBody] ModifyCurrencyPaymentChannelCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.CurrencyPaymentChannelDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteCurrencyPaymentChannelCommand { Id = id };
        return await _mediator.Send(command);
    }
}