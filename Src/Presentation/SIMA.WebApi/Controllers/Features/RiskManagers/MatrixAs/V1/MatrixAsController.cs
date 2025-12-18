using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.MatrixAs;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.MatrixAs.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/MatrixAs")]
public class MatrixAsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MatrixAsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.MatrixAPost)]
    public async Task<Result> Post([FromBody] CreateMatrixACommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.MatrixAPut)]
    public async Task<Result> Put([FromBody] ModifyMatrixACommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.MatrixADelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteMatrixACommand { Id = id };
        return await _mediator.Send(command);
    }
}