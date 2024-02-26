using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BranchManagement.PaymentTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.PaymentTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "PaymentTypes")]
[Authorize]
public class PaymentTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.PaymentTypePost)]
    public async Task<Result> Post([FromBody] AddPaymentTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.PaymentTypePut)]
    public async Task<Result> Put([FromBody] EditPaymentTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.PaymentTypeDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeletePaymentTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}
