using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.Diagrams;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.Diagrams.V1;

[Route("riskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Diagrams")]
[Authorize]
public class RiskDiagramsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    //ToDo Change Name 
    public RiskDiagramsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("riskEvaluation/{id}")]
    [SimaAuthorize(Permissions.RiskDiagrams)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetrRskEvaluationQuery { RiskId = id };
        return await _mediator.Send(query);
    }
    
    [HttpGet("InherentOccurrenceProbabilityChart/{id}")]
    //[SimaAuthorize(Permissions.RiskDiagrams)]
    public async Task<Result> GetInherentOccurrenceProbability([FromRoute] long id)
    {
        var query = new GetInherentOccurrenceProbabilityQuery() { RiskId = id };
        return await _mediator.Send(query);
    }
    
    
    [HttpGet("MatrixAChartChart/{id}")]
    //[SimaAuthorize(Permissions.RiskDiagrams)]
    public async Task<Result> GetMatrixAChartChart([FromRoute] long id)
    {
        var query = new GetMatrixAChartQuery() { RiskId = id };
        return await _mediator.Send(query);
    }
    
    
    [HttpGet("RiskLevelCobitchartChart/{id}")]
    //[SimaAuthorize(Permissions.RiskDiagrams)]
    public async Task<Result> GetRiskLevelCobitchartChart([FromRoute] long id)
    {
        var query = new GetRiskLevelCobitQuery() { RiskId = id };
        return await _mediator.Send(query);
    }
    
    [HttpGet("SeverityChart/{id}")]
    //[SimaAuthorize(Permissions.RiskDiagrams)]
    public async Task<Result> GetSeverityChart([FromRoute] long id)
    {
        var query = new GetSeverityQuery() { RiskId = id };
        return await _mediator.Send(query);
    }
    [HttpGet("CurrentOccurrenceProbabilityChart/{id}")]
    //[SimaAuthorize(Permissions.RiskDiagrams)]
    public async Task<Result> GetCurrentOccurrenceProbabilityChart([FromRoute] long id)
    {
        var query = new GetCurrentOccurrenceProbabilityQuery() { RiskId = id };
        return await _mediator.Send(query);
    }

    
    
}
