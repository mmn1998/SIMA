using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Helpers;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.Helpers.V1;

[Route("serviceCatalog/Helpers")]
[ApiController]
[ApiExplorerSettings(GroupName = "ServiceCatalogeHelpers")]
public class ServiceCatalogeHelperQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceCatalogeHelperQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("last-code/{type}")]
    public async Task<Result> GetLastCode([FromRoute] string type)
    {
        var query = new GetLastCodeQuery { Type = type };
        return await _mediator.Send(query);
    }
}