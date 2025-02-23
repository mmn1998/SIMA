using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Helpers;
using SIMA.Framework.Common.Response;
using SIMA.Persistance.Migrations;
using SIMA.WebApi.Dtos.Helpers;
using System.Text;

namespace SIMA.WebApi.Controllers.Features.Auths.Helpers.V1;

[Route("api/[controller]")]
[ApiController]
public class HelperController : ControllerBase
{
    private readonly IMediator _mediator;

    public HelperController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public Result<string> GetBase64([FromBody] GetBase64Request request)
    {
        if (!string.IsNullOrEmpty(request.Value))
        {
            byte[] byteArrayOfValue = Encoding.UTF8.GetBytes(request.Value);
            var base64 = Convert.ToBase64String(byteArrayOfValue);
            return Result.Ok(base64);
        }
        return Result.Ok(string.Empty);
    }
    [HttpGet("GetHelpDocument")]
    public async Task<Result> GetHelpDocument()
    {
        var query = new GetHelpDocumentQuery();
        return await _mediator.Send(query);
    }
}
