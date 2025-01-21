using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.ConfigurationItems.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/ConfigurationItems")]
[Authorize]
public class ConfigurationItemsQueryController : Controller
{
    private readonly IMediator _mediator;

    public ConfigurationItemsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllConfigurationItemsQuery query)
    {
        return await _mediator.Send(query);
    }
}
