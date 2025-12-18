using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAs;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.MatrixAs.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/MatrixAs")]
public class MatrixAsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public MatrixAsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.MatrixAGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetMatrixAQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.MatrixAGetAll)]
    public async Task<Result> Get([FromBody] GetAllMatrixAsQuery query)
    {
        return await _mediator.Send(query);
    }
}