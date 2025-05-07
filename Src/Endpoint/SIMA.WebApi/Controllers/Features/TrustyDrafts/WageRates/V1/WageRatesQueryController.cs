using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.WageRates;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.WageRates.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/WageRates")]
public class WageRatesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public WageRatesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.WageRatesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetWageRateQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpGet("WageCalculator")]
    //[SimaAuthorize(Permissions.WageRatesGet)]
    public async Task<Result> Get([FromQuery] GetWageCalculatorQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.WageRatesGetAll)]
    public async Task<Result> Get([FromBody] GetAllWageRatesQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("GetAllByCurrencyTypeId/{currencyTypeId}")]
    [SimaAuthorize(Permissions.WageRatesGetAll)]
    public async Task<Result> GetAllByCurrencyTypeId([FromRoute] long currencyTypeId)
    {
        var query = new GetAllWageRatesByCurrencyTypeIdQuery { CurrencyTypeId = currencyTypeId };
        return await _mediator.Send(query);
    }
}