using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BranchManagement.PaymentTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.PaymentTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "PaymentTypes")]
[Authorize]
public class PaymentTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.PaymentTypeGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetPaymentTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpGet]
    [SimaAuthorize(Permissions.PaymentTypeGetAll)]
    public async Task<Result> Get([FromQuery] BaseRequest request)
    {
        var query = new GetAllPaymentTypesQuery { Request = request };
        try
        {
            return await _mediator.Send(query);
        }
        catch (Exception e)
        {

            throw;
        }
    }
}
