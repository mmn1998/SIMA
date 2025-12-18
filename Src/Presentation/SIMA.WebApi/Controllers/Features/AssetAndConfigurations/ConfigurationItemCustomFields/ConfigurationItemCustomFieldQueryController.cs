using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemCustomFields;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.ConfigurationItemCustomFields
{
    [Route("assetAndConfiguration/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Asset/ConfigurationItemCustomFields")]
    public class ConfigurationItemCustomFieldQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigurationItemCustomFieldQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        //[SimaAuthorize(Permissions.ConfigurationItemCustomFieldGet)]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetConfigurationItemCustomFieldQuery { Id = id };
            return await _mediator.Send(query);
        }
        [HttpPost("GetAll")]
        //[SimaAuthorize(Permissions.ConfigurationItemCustomFieldGetAll)]
        public async Task<Result> Get([FromBody] GetAllConfigurationItemCustomFieldsQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
