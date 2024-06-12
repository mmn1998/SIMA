using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.ImportanceDegrees;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ServiceTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ImportanceDegrees")]
public class ImportanceDegreesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImportanceDegreesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateImportanceDegreeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyImportanceDegreeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteImportanceDegreeCommand { Id = id };
        return await _mediator.Send(command);
    }
}