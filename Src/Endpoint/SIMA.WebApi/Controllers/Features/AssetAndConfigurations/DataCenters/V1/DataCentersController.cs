using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.DataCenters;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.DataCenters.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/DataCenters")]
[Authorize]
public class DataCentersController : ControllerBase
{
    private readonly IMediator _mediator;

    public DataCentersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.DataCenterPost)]
    public async Task<Result> Post([FromBody] CreateDataCenterCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
   [SimaAuthorize(Permissions.DataCenterPut)]
    public async Task<Result> Put([FromBody] ModifyDataCenterCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.DataCenterDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteDataCenterCommand { Id = id };
        return await _mediator.Send(command);
    }
}