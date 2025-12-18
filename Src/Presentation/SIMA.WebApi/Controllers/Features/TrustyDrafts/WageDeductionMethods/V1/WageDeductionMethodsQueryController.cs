using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.WageDeductionMethods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.WageDeductionMethods.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/WageDeductionMethods")]
public class WageDeductionMethodsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public WageDeductionMethodsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.WageDeductionMethodGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetWageDeductionMethodQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.WageDeductionMethodGetAll)]
    public async Task<Result> Get([FromBody] GetAllWageDeductionMethodsQuery query)
    {
        return await _mediator.Send(query);
    }
}