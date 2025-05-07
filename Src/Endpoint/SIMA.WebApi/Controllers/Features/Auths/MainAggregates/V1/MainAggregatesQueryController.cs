using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.MainAggregates;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.MainAggregates.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "MainAggregates")]
[Authorize]
public class MainAggregatesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public MainAggregatesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get(GetAllMainAggregateQuery query)
    {
        return await _mediator.Send(query);
    }
}
