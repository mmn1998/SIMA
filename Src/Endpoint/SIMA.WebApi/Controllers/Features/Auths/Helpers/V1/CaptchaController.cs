using Microsoft.AspNetCore.Mvc;
using SIMA.Framework.Common.Cachings;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Framework.WebApi.Services;
using SIMA.Framework.WebApi.Services.Models;
using SIMA.WebApi.Dtos.Captcha;

namespace SIMA.WebApi.Controllers.Features.Auths.Helpers.V1;

[ApiController]
[Route("api/[controller]")]
public class CaptchaController : ControllerBase
{
    private readonly ICaptchaService _captchaService;
    private readonly IDistributedRedisService _redisService;
    private readonly IConfiguration _configuration;
    private readonly IMemoryCacheProvider _cacheProvider;

    public CaptchaController(ICaptchaService captchaService, IDistributedRedisService redisService
        , IConfiguration configuration, IMemoryCacheProvider cacheProvider)
    {
        _captchaService = captchaService;
        _redisService = redisService;
        _configuration = configuration;
        _cacheProvider = cacheProvider;
    }
    [HttpPost("[action]")]
    public async Task<Result> GenerateCaptcha([FromBody] GenerateCaptchaConfigurations? configurations = null)
    {
        if (configurations?.height == 0 || configurations?.width == 0 || configurations?.codeLength == 0 || configurations?.textSize == 0)
        {
            return Result.Failed(new SimaResultException("400", "کانفیگ های ارسالی نباید صفر باشند"));
        }
        var captchaResult = _captchaService.GenerateCaptcha(configurations);
        var id = Guid.NewGuid().ToString();
        string appName = _configuration.GetValue<string>("AppName") ?? "";
        string redisKey = RedisHelper.GenerateRedisKey(appName, "Captcha", id);

        try
        {
            var aliveSeconds = _configuration.GetSection("CaptchaSettings").GetValue<double>("CaptchaAliveSeconds");
            var expirtionTime = TimeSpan.FromSeconds(aliveSeconds);
            await _redisService.InsertAsync(redisKey, captchaResult.Code, expirtionTime: expirtionTime);
        }
        catch
        {
            _cacheProvider.Set(redisKey, captchaResult.Code);
        }

        return Result.Ok(new GenerateCaptchaResponse
        {
            Id = id,
            Image = captchaResult.Image,
        });
    }
    [HttpGet("[action]")]
    public async Task<Result> VerifyCaptcha([FromQuery] VerifyCaptchaRequest request)
    {
        bool result = false;
        string appName = _configuration.GetValue<string>("AppName") ?? "";
        string redisKey = RedisHelper.GenerateRedisKey(appName, "Captcha", request.Id);
        string? captchaCode = "";
        try
        {
            captchaCode = await _redisService.GetAsync(redisKey);
        }
        catch
        {
            captchaCode = _cacheProvider.Get<string>(redisKey);
        }
        if (!string.IsNullOrEmpty(captchaCode))
        {
            var captchaValidRequest = new ValidCaptchaRequest(request.UserInputCode, captchaCode);
            result = _captchaService.ValidateCaptcha(captchaValidRequest);

        }
        else throw SimaResultException.CaptchaError;
        if (!result) throw SimaResultException.CaptchaError;
        return Result.Ok(result);
    }
}
