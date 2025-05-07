using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Categories;
using SIMA.Application.Query.Contract.Features.Auths.CustomeFieldTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.CustomeFieldTypes
{

    [Route("assetAndConfiguration/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Asset/CustomeFieldType")]
    public class CustomeFieldTypesQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomeFieldTypesQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
      
        [HttpPost("GetAll")]
        //[SimaAuthorize(Permissions.CustomeFieldTypeGetAll)]
        public async Task<Result> Get([FromBody] GetAllCustomeFieldTypesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
