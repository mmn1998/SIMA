using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BranchManagement.CurrencyOprationTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.CurrencyOprationTypes;

[Route("branch/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Branch/CurrencyOperationTypes")]
[Authorize]
public class CurrencyOperationTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrencyOperationTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateCurrencyOperationTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyCurrencyOperationTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteCurrencyOperationTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}
