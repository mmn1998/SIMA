using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ImportanceDegrees;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ServiceTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ImportanceDegrees")]
public class ImportanceDegreesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImportanceDegreesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetImportanceDegreeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllImportanceDegreesQuery query)
    {
        return await _mediator.Send(query);
    }
}