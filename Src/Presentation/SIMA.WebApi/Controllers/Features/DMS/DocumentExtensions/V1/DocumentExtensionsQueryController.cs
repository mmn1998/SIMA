using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.DMS.DocumentExtensions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.DMS.DocumentExtensions.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "DocumentExtensions")]
public class DocumentExtensionsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentExtensionsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.DocumentExtensionsGetAll)]
    public async Task<Result> Get(GetAllDocumentExtensionsQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.DocumentExtensionsGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetDocumentExtensionQuery { Id = id };
        return await _mediator.Send(query);
    }
}
