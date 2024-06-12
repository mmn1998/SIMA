//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using SIMA.Application.Query.Contract.Features.Auths.LocationTypes;
//using SIMA.Framework.Common.Request;
//using SIMA.Framework.Common.Response;
//using SIMA.Framework.Infrastructure.Cachings;
//using SIMA.Persistance.Read.Repositories.Features.Auths.LocationTypes;

//namespace SIMA.Persistance.Read.Repositories.Features.Auths.LocationTypes.LocationTypes;

//public class LocationTypeQueryRepositoryCachingDecorator : ILocationTypeQueryRepository
//{
//    private readonly ILocationTypeQueryRepository _repository;
//    private readonly IDistributedRedisService _redisService;
//    private readonly IConfiguration _configuration;
//    private readonly ILogger<LocationTypeQueryRepositoryCachingDecorator> _logger;

//    public LocationTypeQueryRepositoryCachingDecorator(ILocationTypeQueryRepository repository,
//        IDistributedRedisService redisService, IConfiguration configuration, ILogger<LocationTypeQueryRepositoryCachingDecorator> logger)
//    {
//        _repository = repository;
//        _redisService = redisService;
//        _configuration = configuration;
//        _logger = logger;
//    }
//    public async Task<GetLocationTypeQueryResult> FindById(long id)
//    {
//        return await _repository.FindById(id);
//    }

//    public async Task<Result<IEnumerable<GetLocationTypeQueryResult>>> GetAll(GetAllLocationTypeQuery? request = null)
//    {
//        string appName = _configuration.GetSection("AppName").Value ?? "";
//        string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.LocationType);

//        try
//        {
//            var setResult = await _repository.GetAll(new GetAllLocationTypeQuery());
//            _redisService.InsertAsync(redisKey, setResult.Data);
//        }
//        catch (Exception e)
//        {
//            _logger.LogError(e, $"redis insert failed :\n\n{e.Message}");
//        }
//        return await _repository.GetAll(request);
//    }
    
//}
