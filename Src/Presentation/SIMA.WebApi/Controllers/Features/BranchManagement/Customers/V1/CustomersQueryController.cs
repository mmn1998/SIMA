using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BranchManagement.Customers;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.Customers.V1;

[Route("branch/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Branch/Customers")]
public class CustomersQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetCustomerQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllCustomersQuery query)
    {
        return await _mediator.Send(query);
    }
}