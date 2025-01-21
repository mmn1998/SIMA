using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.Assets;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/Assets")]
[Authorize]
public class AssetsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllAssetsQuery query)
    {
        return await _mediator.Send(query);
    }
}