using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Forms;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.Forms.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Forms")]
[Authorize]
public class FormsQueryController : ControllerBase
{

    private readonly IMediator _mediator;

    public FormsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.GenderGet)]
    public async Task<Result> Get(long id)
    {
        var query = new GetFormQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.FormGetAll)]
    public async Task<Result> Get(GetAllFormQuery request)
    {
        return await _mediator.Send(request);

    }
    [HttpPost("FormFields")]
    //[SimaAuthorize(Permissions.FormGetAll)]
    public async Task<Result> Get(GetAllFormFieldsQuery request)
    {
        return await _mediator.Send(request);

    }
}
