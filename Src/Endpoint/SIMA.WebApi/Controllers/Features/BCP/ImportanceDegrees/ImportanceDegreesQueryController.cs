using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ImportanceDegrees;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ServiceTypes.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/ImportanceDegrees")]
public class ImportanceDegreesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImportanceDegreesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.importanceDegreeGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetImportanceDegreeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.importanceDegreeGetAll)]
    public async Task<Result> Get([FromBody] GetAllImportanceDegreesQuery query)
    {
        return await _mediator.Send(query);
    }
}