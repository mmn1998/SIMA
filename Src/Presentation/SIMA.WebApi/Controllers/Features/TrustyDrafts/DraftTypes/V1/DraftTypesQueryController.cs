using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftTypes.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftTypes")]
public class DraftTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.DraftTypesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetDraftTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.DraftTypesGetAll)]
    public async Task<Result> Get([FromBody] GetAllDraftTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}