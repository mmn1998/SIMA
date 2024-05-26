using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.Companies;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Companies.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Companies")]
[Authorize]

public class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]

    [SimaAuthorize(Permissions.CompanyPost)]
    public async Task<Result> Post([FromBody] CreateCompanyCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPut]
    [SimaAuthorize(Permissions.CompanyPut)]
    public async Task<Result> Put([FromBody] ModifyCompanyCommands command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.CompanyDelete)]
    public async Task<Result> Delete(long id)
    {
        var command = new DeleteCompanyCommand { Id = id };
        return await _mediator.Send(command);
    }


}
