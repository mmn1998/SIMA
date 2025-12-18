using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.WageDeductionMethods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.WageDeductionMethods.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/WageDeductionMethods")]
public class WageDeductionMethodsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WageDeductionMethodsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.WageDeductionMethodPost)]
    public async Task<Result> Post([FromBody] CreateWageDeductionMethodCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.WageDeductionMethodPut)]
    public async Task<Result> Put([FromBody] ModifyWageDeductionMethodCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.WageDeductionMethodDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteWageDeductionMethodCommand { Id = id };
        return await _mediator.Send(command);
    }
}