using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.SolutionPeriorities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.SolutionPeriorities.V1;

//[Route("[controller]")]
//[ApiController]
//[ApiExplorerSettings(GroupName = "SolutionPeriorities")]
//public class SolutionPerioritiesQueryController : ControllerBase
//{
//    private readonly IMediator _mediator;

//    public SolutionPerioritiesQueryController(IMediator mediator)
//    {
//        _mediator = mediator;
//    }
//    [HttpGet("{id}")]
//    public async Task<Result> Get([FromRoute] long id)
//    {
//        var query = new GetSolutionPeriorityQuery { Id = id };
//        return await _mediator.Send(query);
//    }
//    [HttpPost("GetAll")]
//    public async Task<Result> Get([FromBody] GetAllSolutionPerioritiesQuery query)
//    {
//        return await _mediator.Send(query);
//    }
//}