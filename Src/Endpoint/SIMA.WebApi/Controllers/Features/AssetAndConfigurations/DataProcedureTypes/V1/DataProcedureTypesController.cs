using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.DataProcedureTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.DataProcedureTypes.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/DataProcedureTypes")]
[Authorize]
public class DataProcedureTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DataProcedureTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    // [SimaAuthorize(Permissions.DataProcedureTypePost)]
    public async Task<Result> Post([FromBody] CreateDataProcedureTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    // [SimaAuthorize(Permissions.DataProcedureTypePut)]
    public async Task<Result> Put([FromBody] ModifyDataProcedureTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.DataProcedureTypeDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteDataProcedureTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}