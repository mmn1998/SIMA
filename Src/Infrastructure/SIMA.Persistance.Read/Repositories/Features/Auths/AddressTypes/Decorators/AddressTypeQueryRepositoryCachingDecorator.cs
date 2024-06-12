//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using SIMA.Application.Query.Contract.Features.Auths.AddressTypes;
//using SIMA.Framework.Common.Request;
//using SIMA.Framework.Common.Response;
//using SIMA.Framework.Infrastructure.Cachings;
//using SIMA.Persistance.Read.Repositories.Features.Auths.AddressTypes;

//namespace SIMA.Persistance.Read.Repositories.Features.Auths.AddressTypes.Decorators;

//public class AddressTypeQueryRepositoryCachingDecorator : IAddressTypeQueryRepository
//{
//    private readonly IAddressTypeQueryRepository _repository;
//    private readonly IConfiguration _configuration;
//    private readonly IDistributedRedisService _redisService;
//    private readonly ILogger<AddressTypeQueryRepositoryCachingDecorator> _logger;

//    public AddressTypeQueryRepositoryCachingDecorator(IAddressTypeQueryRepository repository, IConfiguration configuration,
//        IDistributedRedisService redisService, ILogger<AddressTypeQueryRepositoryCachingDecorator> logger)
//    {
//        _repository = repository;
//        _configuration = configuration;
//        _redisService = redisService;
//        _logger = logger;
//    }
//    public async Task<GetAddressTypeQueryResult> FindById(long id)
//    {
//        return await _repository.FindById(id);
//    }

//    public async Task<Result<List<GetAddressTypeQueryResult>>> GetAll(GetAllAddressTypesQuery baseRequest)
//    {
//        string appName = _configuration.GetSection("AppName").Value ?? "";
//        string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.AddressTypes);
//        try
//        {
//            var setResult = await _repository.GetAllForRedis();
//            _redisService.InsertAsync(redisKey, setResult);
//        }
//        catch (Exception e)
//        {
//            _logger.LogError(e, $"redis insert failed :\n\n{e.Message}");
//        }
//        return await _repository.GetAll(baseRequest);
//    }

//    public async Task<List<GetAddressTypeQueryResult>> GetAllForRedis()
//    {
//        return await _repository.GetAllForRedis();
//    }
//}
