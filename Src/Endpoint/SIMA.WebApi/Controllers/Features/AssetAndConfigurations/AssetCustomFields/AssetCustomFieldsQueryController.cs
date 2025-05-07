using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetCustomFields;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.AssetCustomFields
{

    [Route("assetAndConfiguration/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Asset/AssetCustomFields")]
    public class AssetCustomFieldsQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetCustomFieldsQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetAssetCustomFieldQuery { Id = id };
            return await _mediator.Send(query);
        }
        [HttpPost("GetAll")]
        public async Task<Result> Get([FromBody] GetAllAssetCustomFieldsQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
