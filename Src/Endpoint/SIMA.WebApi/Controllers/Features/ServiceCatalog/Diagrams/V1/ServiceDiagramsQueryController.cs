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
    [SimaAuthorize(Permissions.getServiceTreeDiagram)]
    public async Task<Result> GetServiceTreeDiagram([FromQuery] long? id = null, [FromQuery] int? type = null)
    {
        var query = new GetServiceTreeDiagramQuery { Id = id, Type = type };
        return await _mediator.Send(query);
    }
    [HttpGet("getServiceNetworkDiagram")]
    /*
    [SimaAuthorize(Permissions.getServiceNetworkDiagram)]
    */
    public async Task<Result> GetServiceNetworkDiagram([FromQuery] long? id = null, [FromQuery] string? type = null)
    {
        var query = new GetServiceNetworkDiagramQuery { Id = id, Type = type };
        return await _mediator.Send(query);
    }
    
    [HttpGet("getProdoctList")]
    /*
    [SimaAuthorize(Permissions.getServiceNetworkDiagram)]
    */
    public async Task<Result> GetProdoctList([FromQuery] long? id = null, [FromQuery] string? type = null)
    {
        var query = new GetProductListDiagram() { Id = id, Type = type };
        return await _mediator.Send(query);
    }
    
    
    [HttpGet("getChannelList")]
    /*
    [SimaAuthorize(Permissions.getServiceNetworkDiagram)]
    */
    public async Task<Result> GetChannelList([FromQuery] long? id = null, [FromQuery] string? type = null)
    {
        var query = new GetChannelListDiagram { Id = id, Type = type };
        return await _mediator.Send(query);
    }
    
    [HttpGet("getAssetList")]
    /*
    [SimaAuthorize(Permissions.getServiceNetworkDiagram)]
    */
    public async Task<Result> GetAssetList([FromQuery] long? id = null, [FromQuery] string? type = null)
    {
        var query = new GetAssetListDiagram { Id = id, Type = type };
        return await _mediator.Send(query);
    }
    
    
    [HttpGet("getAssignedStaffList")]
    /*
    [SimaAuthorize(Permissions.getServiceNetworkDiagram)]
    */
    public async Task<Result> GetAssignedStaffList([FromQuery] long? id = null, [FromQuery] string? type = null)
    {
        var query = new GetAssignedStaffListDiagram { Id = id, Type = type };
        return await _mediator.Send(query);
    }

    [HttpGet("getApiList")]
    /*
    [SimaAuthorize(Permissions.getServiceNetworkDiagram)]
    */
    public async Task<Result> GetApiList([FromQuery] long? id = null, [FromQuery] string? type = null)
    {
        var query = new GetApiListDiagram { Id = id, Type = type };
        return await _mediator.Send(query);
    }
    
    [HttpGet("getProcedureList")]
    /*
    [SimaAuthorize(Permissions.getServiceNetworkDiagram)]
    */
    public async Task<Result> GetProcedureList([FromQuery] long? id = null, [FromQuery] string? type = null)
    {
        var query = new GetProcedureListDiagram { Id = id, Type = type };
        return await _mediator.Send(query);
    }
    
    [HttpGet("getRiskList")]
    /*
    [SimaAuthorize(Permissions.getServiceNetworkDiagram)]
    */
    public async Task<Result> GetRiskList([FromQuery] long? id = null, [FromQuery] string? type = null)
    {
        var query = new GetRiskListDiagram { Id = id, Type = type };
        return await _mediator.Send(query);
    }
    
    [HttpGet("getConfigurationItemList")]
    /*
    [SimaAuthorize(Permissions.getServiceNetworkDiagram)]
    */
    public async Task<Result> GetConfigurationItemList([FromQuery] long? id = null, [FromQuery] string? type = null)
    {
        var query = new GetConfigurationItemListDiagram { Id = id, Type = type };
        return await _mediator.Send(query);
    }
}