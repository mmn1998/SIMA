using Microsoft.AspNetCore.Mvc;
using SIMA.Domain.Settings;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.Helpers.V1;

[Route("api/[controller]")]
[ApiController]
public class SystemConfigsController : ControllerBase
{
    [HttpGet("GetSystemParamters")]
    public async Task<Result> Get()
    {
        var @params = SystemParams.SystemParameters.ToList();
        return Result.Ok(@params);
    }
    [HttpGet("GenerateCode")]
    public async Task<Result> GenerateCode()
    {
        var code = CodeGenerator.GeneratedCode();   
        return Result.Ok(code);
    }
}
