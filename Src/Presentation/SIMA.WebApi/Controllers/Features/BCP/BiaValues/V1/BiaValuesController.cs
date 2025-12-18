using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.BiaValues;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.BiaValues.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/BiaValues")]
public class BiaValuesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BiaValuesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateBiaValueCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyBiaValueCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteBiaValueCommand { Id = id };
        return await _mediator.Send(command);
    }
}