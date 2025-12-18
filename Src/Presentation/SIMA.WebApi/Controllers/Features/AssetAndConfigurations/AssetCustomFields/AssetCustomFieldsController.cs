using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.AssetCustomFields;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.AssetCustomFields
{
    [Route("assetAndConfiguration/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Asset/AssetCustomFields")]

    public class AssetCustomFieldsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetCustomFieldsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<Result> Post([FromBody] CreateAssetCustomFieldCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        public async Task<Result> Put([FromBody] ModifyAssetCustomFieldCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteAssetCustomFieldCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
