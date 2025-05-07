using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.Cartables;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

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
    /// <summary>
    /// کارتاپل درخواست های تدارکات
    /// </summary>
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.LogisticCartableGetAll)]
    public async Task<Result> GetAll(LogisticCartableGetAllQuery request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// درخواست های تدارکات من
    /// </summary>
    [HttpPost("MyLogisticsRequestList")]
    [SimaAuthorize(Permissions.LogisticsRequestsGetAll)]
    public async Task<Result> MyLogisticsRequestList(GetAllLogisticsRequestsQuery query)
    {
        return await _mediator.Send(query);
    }

    /// <summary>
    /// جزئیات تدارکات
    /// </summary>
    [HttpGet("{id}/{issueId}")]
    [SimaAuthorize(Permissions.LogisticsRequestsGet)]
    public async Task<Result> Get([FromRoute] long id, [FromRoute] long issueId)
    {
        var query = new LogisticCartableGetQuery { Id = id, IssueId = issueId };
        var result = await _mediator.Send(query);
        foreach (var document in result.Data.DocumentList)
        {
            document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
        }
        //foreach (var document in result.Data.InvoiceDocumentList)
        //{
        //    document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
        //}
        //foreach (var document in result.Data.ReceiptDocumentList)
        //{
        //    document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
        //}
        return result;
    }
}
