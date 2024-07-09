using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.Cartables;
using SIMA.Application.Query.Contract.Features.Logistics.GoodsCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.WebApi.Extensions;

namespace SIMA.WebApi.Controllers.Features.Logistics.Cartables;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "LogisticCartable")]
[Authorize]

public class LogisticCartableQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public LogisticCartableQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.LogisticCartableGetAll)]
    public async Task<Result> Get(LogisticCartableGetAllQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}/{issueId}")]
    [SimaAuthorize(Permissions.GoodsCategoriesGet)]
    public async Task<Result> Get([FromRoute] long id, [FromRoute] long issueId)
    {
        var query = new LogisticCartableGetQuery { Id = id, IssueId = issueId };
        var result = await _mediator.Send(query);
        foreach (var document in result.Data.DocumentList)
        {
            document.DocumentContentType = document.DocumentExtensionName.GetContentType();
        }
        foreach (var document in result.Data.InvoiceDocumentList)
        {
            document.DocumentContentType = document.DocumentExtensionName.GetContentType();
        }
        foreach (var document in result.Data.ReceiptDocumentList)
        {
            document.DocumentContentType = document.DocumentExtensionName.GetContentType();
        }
        return result;
    }
}
