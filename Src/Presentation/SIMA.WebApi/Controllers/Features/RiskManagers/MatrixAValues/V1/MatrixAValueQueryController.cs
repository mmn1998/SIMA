using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;
using SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.MatrixAValues.V1;


[ApiController]
[Route("riskManagement/[controller]")]
[Authorize]
[ApiExplorerSettings(GroupName = "RiskManagement/MatrixAValue")]
public class MatrixAValueQueryController : ControllerBase
{  
    private readonly IMediator _mediator;

    public MatrixAValueQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaGet)]
    */
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetMatrixAValueQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaGetAll)]
    */
    public async Task<Result> Get([FromBody] GetAllMatrixAValuesQuery query)
    {
        return await _mediator.Send(query);
    }
    
}