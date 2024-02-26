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
    [HttpGet]
    [SimaAuthorize(Permissions.DocumentExtensionsGetAll)]
    public async Task<Result> Get([FromQuery] BaseRequest request)
    {
        var command = new GetAllDocumentExtensionsQuery { Request = request };
        return await _mediator.Send(command);
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.DocumentExtensionsGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var command = new GetDocumentExtensionQuery { Id = id };
        return await _mediator.Send(command);
    }
}
