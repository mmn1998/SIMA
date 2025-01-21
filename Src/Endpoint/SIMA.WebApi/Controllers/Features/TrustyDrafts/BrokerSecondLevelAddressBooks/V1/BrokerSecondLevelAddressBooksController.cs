using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.BrokerSecondLevelAddressBooks;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/BrokerSecondLevelAddressBooks")]
public class BrokerSecondLevelAddressBooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BrokerSecondLevelAddressBooksController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.BrokerSecondLevelAddressBookPost)]
    public async Task<Result> Post([FromBody] CreateBrokerSecondLevelAddressBookCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.BrokerSecondLevelAddressBookPut)]
    public async Task<Result> Put([FromBody] ModifyBrokerSecondLevelAddressBookCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.BrokerSecondLevelAddressBookDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteBrokerSecondLevelAddressBookCommand { Id = id };
        return await _mediator.Send(command);
    }
}