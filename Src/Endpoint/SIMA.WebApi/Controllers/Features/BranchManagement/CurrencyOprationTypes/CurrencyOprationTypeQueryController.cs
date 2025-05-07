using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyOprationTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.CurrencyOprationTypes;

[Route("branch/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Branch/CurrencyOperationTypes")]
[Authorize]
public class CurrencyOperationTypeQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrencyOperationTypeQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get(GetAllCurrencyOprationTypesQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetCurrencyOprationTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
}
