using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.EvaluationCriterias;
using SIMA.Application.Contract.Features.RiskManagers.MatrixAValues;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.MatrixAValues.V1;

[ApiController]
[Route("riskManagement/[controller]")]
[Authorize]
[ApiExplorerSettings(GroupName = "RiskManagement/MatrixAValue")]
public class MatrixAValueController : ControllerBase
{
    private readonly IMediator _mediator;

    public MatrixAValueController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaPost)]
    */
    public async Task<Result> Post([FromBody] CreateMatrixAValuesCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaPut)]
    */
    public async Task<Result> Put([FromBody] ModifyMatrixAValuesCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaDelete)]
    */
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteMatrixAValuesCommand { Id = id };
        return await _mediator.Send(command);
    }
}