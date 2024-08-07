using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.DataTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.DataTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "DataTypes")]
[Authorize]
public class DataTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DataTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<Result> Get()
    {
        var query = new GetDataTypeQuery();
        return await _mediator.Send(query);
    }
}