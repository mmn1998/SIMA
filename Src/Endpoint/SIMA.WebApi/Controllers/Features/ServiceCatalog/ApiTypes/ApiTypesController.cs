using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.ServiceCatalog.ApiTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ApiTypes;

[Route("serviceCatalog/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ApiTypes")]
[Authorize]
public class ApiTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApiTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.ApiTypePost)]
    public async Task<Result> Post([FromBody] CreateApiTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.ApiTypePut)]
    public async Task<Result> Put([FromBody] ModifyApiTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.ApiTypeDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteApiTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}