using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.BrokerSecondLevelAddressBooks;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/BrokerSecondLevelAddressBooks")]
public class BrokerSecondLevelAddressBooksQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public BrokerSecondLevelAddressBooksQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.BrokerSecondLevelAddressBookGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetBrokerSecondLevelAddressBookQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.BrokerSecondLevelAddressBookGetAll)]
    public async Task<Result> Get([FromBody] GetAllBrokerSecondLevelAddressBooksQuery query)
    {
        return await _mediator.Send(query);
    }
}