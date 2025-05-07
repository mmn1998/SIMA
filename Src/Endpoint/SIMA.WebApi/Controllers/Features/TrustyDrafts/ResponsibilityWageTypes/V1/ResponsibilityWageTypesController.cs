using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.ResponsibilityWageTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.ResponsibilityWageTypes.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/ResponsibilityWageTypes")]
public class ResponsibilityWageTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResponsibilityWageTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.ResponsibilityWageTypesPost)]
    public async Task<Result> Post([FromBody] CreateResponsibilityWageTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.ResponsibilityWageTypesPut)]
    public async Task<Result> Put([FromBody] ModifyResponsibilityWageTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.ResponsibilityWageTypesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteResponsibilityWageTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}