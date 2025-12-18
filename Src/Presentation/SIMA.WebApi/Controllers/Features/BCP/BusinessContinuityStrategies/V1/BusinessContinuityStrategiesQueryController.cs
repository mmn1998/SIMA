using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityStrategies;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.BusinessContinuityStrategies.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/BusinessContinuityStrategies")]
public class BusinessContinuityStrategiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public BusinessContinuityStrategiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.businessContinuityStratgyGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetBusinessContinuityStrategyQuery { Id = id };
        var result = await _mediator.Send(query);
        if (result.Data.BusinessContinuityStrategyDocumentList is not null)
        {
            foreach (var document in result.Data.BusinessContinuityStrategyDocumentList)
            {
                document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
            }
        }
        return result;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.businessContinuityStratgyGetAll)]
    public async Task<Result> Get([FromBody] GetAllBusinessContinuityStrategiesQuery query)
    {
        return await _mediator.Send(query);
    }
}