using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ServiceCategories.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ServiceCategories")]
public class ServiceCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.ServiceCategoryPost)]
    public async Task<Result> Post([FromBody] CreateServiceCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.ServiceCategoryPut)]
    public async Task<Result> Put([FromBody] ModifyServiceCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.ServiceCategoryDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteServiceCategoryCommand { Id = id };
        return await _mediator.Send(command);
    }
}