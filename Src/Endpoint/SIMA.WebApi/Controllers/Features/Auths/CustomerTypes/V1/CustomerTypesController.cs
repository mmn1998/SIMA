using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.CustomerTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.CustomerTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "CustomerTypes")]
public class CustomerTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.customerTypePost)]
    public async Task<Result> Post([FromBody] CreateCustomerTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.customerTypePut)]
    public async Task<Result> Put([FromBody] ModifyCustomerTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.customerTypeDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteCustomerTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}