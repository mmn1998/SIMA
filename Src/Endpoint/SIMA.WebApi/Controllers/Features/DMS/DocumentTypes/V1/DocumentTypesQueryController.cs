using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.DMS.DocumentTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.DMS.DocumentTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "DocumentTypes")]
public class DocumentTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [SimaAuthorize(Permissions.DocumentTypesGetAll)]
    public async Task<Result> Get([FromQuery] BaseRequest request)
    {
        var command = new GetAllDocumentTypesQuery { Request = request };
        return await _mediator.Send(command);
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.DocumentTypesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var command = new GetDocumentTypeQuery { Id = id };
        return await _mediator.Send(command);
    }
}
