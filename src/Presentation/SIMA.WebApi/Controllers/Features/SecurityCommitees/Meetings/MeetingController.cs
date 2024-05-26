using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.SecurityCommitees.Meetings;
using SIMA.Application.Contract.Features.SecurityCommitees.SubjectPriorities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.SecurityCommitees.Meetings
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Meeting")]
    public class MeetingController : Controller
    {
        private readonly IMediator _mediator;

        public MeetingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<Result> Post([FromBody] CreateMeetingCommands command)
        {
            return await _mediator.Send(command);
        }
    }
}


    