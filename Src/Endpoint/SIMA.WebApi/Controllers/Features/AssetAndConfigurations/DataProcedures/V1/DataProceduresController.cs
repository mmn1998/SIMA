using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.DataProcedures;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.DataProcedures.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/DataProcedures")]
[Authorize]
public class DataProceduresController : ControllerBase
{
    private readonly IMediator _mediator;

    public DataProceduresController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    // [SimaAuthorize(Permissions.DataProcedurePost)]
    public async Task<Result> Post([FromBody] CreateDataProcedureCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    // [SimaAuthorize(Permissions.DataProcedurePut)]
    public async Task<Result> Put([FromBody] ModifyDataProcedureCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.DataProcedureDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteDataProcedureCommand { Id = id };
        return await _mediator.Send(command);
    }
}
