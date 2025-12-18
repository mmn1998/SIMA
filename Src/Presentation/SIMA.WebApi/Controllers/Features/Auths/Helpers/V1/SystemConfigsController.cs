using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Domain.Settings;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using System.Reflection;

namespace SIMA.WebApi.Controllers.Features.Auths.Helpers.V1;

[Route("api/[controller]")]
[ApiController]
/*[Authorize]*/
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
    [AllowAnonymous]
    [HttpGet("GetVersion")]
    public async Task<Result> GetVersion()
    {
        var appVersion = typeof(Program).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        var result = new {  appVersion = appVersion?.Split('+')[0], metaData = appVersion?.Split('+')[1] };
        return Result.Ok(result);
    } 
}
