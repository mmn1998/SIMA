using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.ResponsibilityWageTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.ResponsibilityWageTypes.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/ResponsibilityWageTypes")]
public class ResponsibilityWageTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResponsibilityWageTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.ResponsibilityWageTypesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetResponsibilityWageTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.ResponsibilityWageTypesGetAll)]
    public async Task<Result> Get([FromBody] GetAllResponsibilityWageTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}