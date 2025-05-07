using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Services.BehsazanServices.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.TrustyServices;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/TrustyServices")]
public class TrustyServicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrustyServicesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    //[HttpPost]
    //[SimaAuthorize(Permissions.trsu)]
    //public async Task<Result> Post([FromBody] TrustCurrencyDraft command)
    //{
    //    return await _mediator.Send(command);
    //}
}
