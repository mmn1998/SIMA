using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.TrustyDrafts;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/TrustyDrafts")]
[Authorize]
public class TrustyDraftQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrustyDraftQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAllTrustyDraftRequested")]
    [SimaAuthorize(Permissions.TrustyDraftsGetAll)]
    public async Task<Result> Get([FromBody] GetAllTrustyDraftRequested query)
    {
        return await _mediator.Send(query);
    }
    [HttpPost("GetAllMy")]
    [SimaAuthorize(Permissions.TrustyDraftsGetAll)]
    public async Task<Result> Get([FromBody] GetAllMyTrustyDraftsQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpPost("GetAllDraftForPayment")]
    [SimaAuthorize(Permissions.TrustyDraftsGetAll)]
    public async Task<Result> Get([FromBody] GetAllDraftForPayment query)
    {
        return await _mediator.Send(query);
    }

    [HttpPost("GetAllReconcilliation")]
    [SimaAuthorize(Permissions.TrustyDraftsGetAll)]
    public async Task<Result> Get([FromBody] GetAllReconcilliation query)
    {
        return await _mediator.Send(query);
    }

    [HttpPost("GetAllFrorEachDepartment")]
    [SimaAuthorize(Permissions.TrustyDraftsGetAll)]
    public async Task<Result> Get([FromBody] GetAllFrorEachDepartment query)
    {
        return await _mediator.Send(query);
    }

    [HttpPost("GetReport")]
    [SimaAuthorize(Permissions.GetAllReport)]
    public async Task<Result> Get([FromBody] GetTrustyDraftReportQuery query)
    {
        return await _mediator.Send(query);
    }
    
    [HttpGet("GetAllByBroker/{brokerId}")]
    [SimaAuthorize(Permissions.TrustyDraftsGetAll)]
    public async Task<Result> GetAllByBroker([FromRoute] long? brokerId)
    {
        var query = new GetAllTrustyDraftByBrokerQuery { BrokerId = brokerId };
        return await _mediator.Send(query);
    }

    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.TrustyDraftsGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        try
        {
            var query = new GetTrustyDraftQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result.Data.TrustyDraftDocumentList is not null)
            {
                foreach (var document in result.Data.TrustyDraftDocumentList)
                {
                    document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
                }
            }
            return result;

        }
        catch (Exception ex)
        {

            throw;
        }
        

    }

}
