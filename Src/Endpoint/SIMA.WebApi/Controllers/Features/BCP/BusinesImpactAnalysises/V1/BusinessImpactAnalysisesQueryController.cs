using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.BusinesImpactAnalysises;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.BusinesImpactAnalysises.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/BusinessImpactAnalysises")]
public class BusinessImpactAnalysisesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public BusinessImpactAnalysisesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.businessImpactAnalysisGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetBusinessImpactAnalysisQuery { Id = id };
        var result = await _mediator.Send(query);
        if (result.Data.BusinessImpactAnalysisDocumemntList is not null)
        {
            foreach (var document in result.Data.BusinessImpactAnalysisDocumemntList)
            {
                document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
            }
        }
        return result;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.businessImpactAnalysisGetAll)]
    public async Task<Result> Get([FromBody] GetAllBusinessImpactAnalysisesQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpPost("GetAllByServiceId")]
    [SimaAuthorize(Permissions.businessImpactAnalysisGetAllByService)]
    public async Task<Result> GetAllByServiceId([FromBody] GetAllBusinessImpactAnalysisesQuery query)
    {
        return await _mediator.Send(query);
    }
}