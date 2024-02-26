using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Gender;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Persistance.Read.Repositories.Features.Auths.Genders;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Genders.Decorators;

public class GenderQueryRepositoryCachingDecorator : IGenderQueryRepository
{
    private readonly IGenderQueryRepository _repository;
    private readonly IDistributedRedisService _redisService;
    private readonly IConfiguration _configuration;

    public GenderQueryRepositoryCachingDecorator(IGenderQueryRepository repository, IDistributedRedisService redisService,
        IConfiguration configuration)
    {
        _repository = repository;
        _redisService = redisService;
        _configuration = configuration;
    }
    public async Task<GetGenderQueryResult> FindById(long id)
    {
        return await _repository.FindById(id);
    }

    public async Task<Result<List<GetGenderQueryResult>>> GetAll(BaseRequest? baseRequests = null)
    {
        string appName = _configuration.GetSection("AppName").Value ?? "";
        string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.Genders);

        var setResult = await _repository.GetAll();
        _redisService.InsertAsync(redisKey, setResult.Data);


        return await _repository.GetAll(baseRequests);

    }
}
