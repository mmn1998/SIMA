using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.Cartables;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.SecurityCommitees.Cartables.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Cartables")]
public class CartablesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartablesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("GetAll")]
    public async Task<Result> Get(GetAllCartableQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("Detail")]
    public async Task<Result> Get([FromQuery] GetCartableQuery query)
    {
        var result = await _mediator.Send(query);
        foreach (var document in result.Data.IssueDocuments)
        {
            document.DocumentContentType = document.DocumentExtentionName.GetContentType();
        }
        return result;
    }


}
