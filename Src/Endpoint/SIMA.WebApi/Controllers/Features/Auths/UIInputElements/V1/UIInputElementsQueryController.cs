using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.UIInputElements;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.UIInputElements.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "UIInputElements")]
[Authorize]
public class UIInputElementsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public UIInputElementsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.UIInputElementGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetUIInputElementQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.UIInputElementGetAll)]
    public async Task<Result> Get([FromBody] GetAllUIInputElementsQuery query)
    {
        return await _mediator.Send(query);
    }
}