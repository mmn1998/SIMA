using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.AssetTypes;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemCustomFields;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.ConfigurationItemCustomFields
{

    [Route("assetAndConfiguration/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Asset/ConfigurationItemCustomFields")]
    public class ConfigurationItemCustomFieldsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigurationItemCustomFieldsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        //[SimaAuthorize(Permissions.ConfigurationItemCustomFieldGPost)]
        public async Task<Result> Post([FromBody] CreateConfigurationItemCustomFieldCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        //[SimaAuthorize(Permissions.ConfigurationItemCustomFieldPut)]
        public async Task<Result> Put([FromBody] ModifyConfigurationItemCustomFieldCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        //[SimaAuthorize(Permissions.ConfigurationItemCustomFieldDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteConfigurationItemCustomFieldCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
