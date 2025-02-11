namespace SIMA.WebApi.Controllers.Features.BCP.SolutionPeriorities.V1;

//[Route("[controller]")]
//[ApiController]
//[ApiExplorerSettings(GroupName = "SolutionPeriorities")]
//public class SolutionPerioritiesController : ControllerBase
//{
//    private readonly IMediator _mediator;

//    public SolutionPerioritiesController(IMediator mediator)
//    {
//        _mediator = mediator;
//    }
//    [HttpPost]
//    public async Task<Result> Post([FromBody] CreateSolutionPeriorityCommand command)
//    {
//        return await _mediator.Send(command);
//    }
//    [HttpPut]
//    public async Task<Result> Put([FromBody] ModifySolutionPeriorityCommand command)
//    {
//        return await _mediator.Send(command);
//    }
//    [HttpDelete("{id}")]
//    public async Task<Result> Delete([FromRoute] long id)
//    {
//        var command = new DeleteSolutionPeriorityCommand { Id = id };
//        return await _mediator.Send(command);
//    }
//}