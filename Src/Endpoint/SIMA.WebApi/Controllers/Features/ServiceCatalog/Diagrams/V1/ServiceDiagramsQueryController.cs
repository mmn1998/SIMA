using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.Diagrams.V1;

[Route("serviceCatalog/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Diagrams")]
public class ServiceDiagramsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceDiagramsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("getServiceTreeDiagram")]
    //[SimaAuthorize(Permissions.getServiceTreeDiagram)]
    public async Task<Result> GetServiceTreeDiagram([FromQuery] long? id = null, [FromQuery] int? type = null)
    {
        var query = new GetServiceTreeDiagramQuery { Id = id, Type = type };
        return await _mediator.Send(query);
    }
    [HttpGet("getServiceNetworkDiagram")]
    //[SimaAuthorize(Permissions.getServiceNetworkDiagram)]
    public async Task<Result> GetServiceNetworkDiagram([FromQuery] long? id = null, [FromQuery] int? type = null)
    {
        var query = new GetServiceNetworkDiagramQuery { Id = id, Type = type };
        return await _mediator.Send(query);
    }
}