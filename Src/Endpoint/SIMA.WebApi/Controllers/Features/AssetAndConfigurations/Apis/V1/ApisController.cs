using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Apis;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.Apis.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/Apis")]
[Authorize]
public class ApisController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApisController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
     
     [SimaAuthorize(Permissions.APIPost)]
    
    public async Task<Result> Post([FromBody] CreateApiCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.APIPut)]
    public async Task<Result> Put([FromBody] ModifyApiCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.APIDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteApiCommand { Id = id };
        return await _mediator.Send(command);
    }
}
