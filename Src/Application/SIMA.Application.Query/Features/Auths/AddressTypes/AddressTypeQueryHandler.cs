using AutoMapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.AddressTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Persistance.Read.Repositories.Features.Auths.AddressTypes;

namespace SIMA.Application.Query.Features.Auths.AddressTypes;

public class AddressTypeQueryHandler : IQueryHandler<GetAddressTypeQuery, Result<GetAddressTypeQueryResult>>, IQueryHandler<GetAllAddressTypesQuery, Result<List<GetAddressTypeQueryResult>>>
{
    private readonly IAddressTypeQueryRepository _repository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IDistributedRedisService _redisService;

    public AddressTypeQueryHandler(IAddressTypeQueryRepository repository, IMapper mapper,
        IConfiguration configuration, IDistributedRedisService redisService)
    {
        _repository = repository;
        _mapper = mapper;
        _configuration = configuration;
        _redisService = redisService;
    }
    public async Task<Result<GetAddressTypeQueryResult>> Handle(GetAddressTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.FindById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetAddressTypeQueryResult>>> Handle(GetAllAddressTypesQuery request, CancellationToken cancellationToken)
    {
        string appName = _configuration.GetSection("AppName").Value ?? "";
        string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.AddressTypes);
        bool redisResult = false;

        try
        {
            redisResult = _redisService.TryGet(redisKey, out List<GetAddressTypeQueryResult> values);
            if (!redisResult) throw new Exception();

            values = string.IsNullOrEmpty(request.Request.SearchValue) ? values : values.Where(it => it.Name.Contains(request.Request.SearchValue) || it.Code.Contains(request.Request.SearchValue)).ToList();
            int totalCount = values.Count();
            values = values.Skip((request.Request.Skip - 1) * request.Request.Take).Take(request.Request.Take).ToList();
            return Result.Ok(values, totalCount);

        }
        catch (Exception e)
        {
            return await _repository.GetAll(request.Request);
        }
    }
}
