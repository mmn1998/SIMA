using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.WageRates;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.WageRates.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/WageRates")]
public class WageRatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public WageRatesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.WageRatesPost)]
    public async Task<Result> Post([FromBody] CreateWageRateCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.WageRatesPut)]
    public async Task<Result> Put([FromBody] ModifyWageRateCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.WageRatesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteWageRateCommand { Id = id };
        return await _mediator.Send(command);
    }
}