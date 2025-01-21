using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Contract.Features.TrustyDrafts.ReferralLetters;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Resources;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.ReferralLetters;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/ReferralLetters")]
[Authorize]
public class ReferralLettersController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReferralLettersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.ReferralLettersPost)]
    public async Task<Result> Post([FromBody] CreateReferralLetterCommand command)
    {
        var referralLetter =  await _mediator.Send(command);

        var runAction = new IssueRunActionCommand
        {
            IssueId = referralLetter.Data.IssueId,
            ProgressId = referralLetter.Data.ProgressId,
            NextStepId = referralLetter.Data.NextStepId,    
        };

        try
        {
            var resultIssueRunAction = await _mediator.Send(runAction);

            return resultIssueRunAction;
        }
        catch
        {
            var deleteCommand = new DeleteReferralLetterCommand { Id = referralLetter.Data.Id };
            var deleteLetter = await _mediator.Send(deleteCommand);
            throw new SimaResultException(CodeMessges._400Code, Messages.RefferalLetterNotAllowCreate); //CheckMessage
        }
    }
    [HttpPut]
    [SimaAuthorize(Permissions.ReferralLettersPut)]
    public async Task<Result> Put([FromBody] ModifyReferralLetterCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.ReferralLettersDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteReferralLetterCommand { Id = id };
        return await _mediator.Send(command);
    }
}
