using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.CancellationResaons;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.CancellationResaons.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/CancellationResaons")]
public class CancellationResaonsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CancellationResaonsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.cancellationResaonGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetCancellationResaonQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.cancellationResaonGetAll)]
    public async Task<Result> Get([FromBody] GetAllCancellationResaonsQuery query)
    {
        return await _mediator.Send(query);
    }
}