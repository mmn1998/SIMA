using Microsoft.AspNetCore.Mvc;
using SIMA.Framework.Common.Response;
using SIMA.WebApi.Dtos.Helpers;
using System.Text;

namespace SIMA.WebApi.Controllers.Features.Auths.Helpers.V1;

[Route("api/[controller]")]
[ApiController]
public class HelperController : ControllerBase
{
    [HttpPost]
    public Result<string> GetBase64([FromBody] GetBase64Request request)
    {
        if (!string.IsNullOrEmpty(request.Value))
        {
            byte[] byteArrayOfValue = Encoding.UTF8.GetBytes(request.Value);
            var base64 = Convert.ToBase64String(byteArrayOfValue);
            return Result.Ok(base64);
        }
        return Result.Ok(string.Empty);
    }
}
