using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.ReferalLetters;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.WebApi.Extensions;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.ReferralLetters;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/ReferralLetters")]
[Authorize]
public class ReferralLettersQueryController : Controller
{
    private readonly IMediator _mediator;

    public ReferralLettersQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.ReferralLettersGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetReferalLetterQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpGet("GenerateEceFile/{letterNumber}")]
    [SimaAuthorize(Permissions.ReferralLettersGet)]
    public async Task<Result> Get([FromRoute] string letterNumber)
    {
        var query = new GetReferalLetterQueryByLetterNumber { LetterNumber = letterNumber };            
        var result = await _mediator.Send(query);
        result.Data.ContentType = result.Data.Extension.GetContentType();
        return result;
    }
    [HttpPost("getAll")]
    [SimaAuthorize(Permissions.ReferralLettersGetAll)]
    public async Task<Result> Get([FromBody] GetAllReferalLettersQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpPost("getAllLetterToSecretariat")]
    [SimaAuthorize(Permissions.GetAllLetterToSecretariat)]
    public async Task<Result> Get([FromBody] GetAllReferralLetterToSecretariatQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpPost("getAllLetterToExchange")]
    [SimaAuthorize(Permissions.GetAllLetterToExchange)]
    public async Task<Result> Get([FromBody] GetAllReferralLetterToExchangeQuery query)
    {
        return await _mediator.Send(query);
    }
}
