using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.ServiceCatalog.ApiTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ApiTypes
{
    [Route("serviceCatalog/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApiTypes")]
    [Authorize]
    public class ApiTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApiTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<Result> Post([FromBody] CreateApiTypeCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        public async Task<Result> Put([FromBody] ModifyApiTypeCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteApiTypeCommand { Id = id };
            return await _mediator.Send(command);
        }


    }
}
