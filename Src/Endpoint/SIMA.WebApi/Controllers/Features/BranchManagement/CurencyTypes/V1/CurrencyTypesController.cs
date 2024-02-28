using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BranchManagement.CurrencyTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.CurencyTypes.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "CurrencyTypes")]
    [Authorize]
    public class CurrencyTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [SimaAuthorize(Permissions.CurrencyTypePost)]
        public async Task<Result> Post([FromBody] CreateCurrencyTypeCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        [SimaAuthorize(Permissions.CurrencyTypePut)]
        public async Task<Result> Put([FromBody] ModifyCurrencyTypeCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.CurrencyTypeDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteCurrencyTypeCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
