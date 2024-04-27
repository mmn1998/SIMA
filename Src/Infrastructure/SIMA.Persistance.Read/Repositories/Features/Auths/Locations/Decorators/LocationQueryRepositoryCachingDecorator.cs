using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SIMA.Application.Query.Contract.Features.Auths.Locations;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Persistance.Read.Repositories.Features.Auths.Locations;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.Locations.Decorators;

public class LocationQueryRepositoryCachingDecorator : ILocationQueryRepository
{
    private readonly ILocationQueryRepository _queryRepository;
    private readonly IDistributedRedisService _redisService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<LocationQueryRepositoryCachingDecorator> _logger;

    public LocationQueryRepositoryCachingDecorator(ILocationQueryRepository queryRepository, IDistributedRedisService redisService,
        IConfiguration configuration, ILogger<LocationQueryRepositoryCachingDecorator> logger)
    {
        _queryRepository = queryRepository;
        _redisService = redisService;
        _configuration = configuration;
        _logger = logger;
    }
    public async Task<GetLocationQueryResult> FindById(long id)
    {
        return await _queryRepository.FindById(id);
    }

    public async Task<Result<IEnumerable<GetLocationQueryResult>>> GetAll(GetAllLocationQuery? request = null)
    {
        string appName = _configuration.GetSection("AppName").Value ?? "";
        string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.Location);
        try
        {
            var setResult = await _queryRepository.GetAll(new GetAllLocationQuery());
            _redisService.InsertAsync(redisKey, setResult.Data);

        }
        catch (Exception e)
        {
            _logger.LogError(e, $"redis insert failed :\n\n{e.Message}");
        }

        return await _queryRepository.GetAll(request);
    }

    public async Task<List<GetParentLocationsByLocationTypeIdQueryResult>> GetParentsByChildId(long locationTypeId)
    {
        return await _queryRepository.GetParentsByChildId(locationTypeId);
    }
}
