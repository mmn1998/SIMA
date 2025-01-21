using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.ReconsilationTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.ReconsilationTypes.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/ReconciliationTypes")]
public class ReconciliationTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReconciliationTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.ReconciliationTypesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetReconsilationTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.ReconciliationTypesGetAll)]
    public async Task<Result> Get([FromBody] GetAllReconsilationTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}