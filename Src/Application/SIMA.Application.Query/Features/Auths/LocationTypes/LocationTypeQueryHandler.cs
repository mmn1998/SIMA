using AutoMapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.LocationTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Persistance.Read.Repositories.Features.Auths.LocationTypes;

namespace SIMA.Application.Query.Features.Auths.LocationTypes;

public class LocationTypeQueryHandler : IQueryHandler<GetLocationTypeQuery, Result<GetLocationTypeQueryResult>>, IQueryHandler<GetAllLocationTypeQuery, Result<List<GetLocationTypeQueryResult>>>
{
    private readonly IMapper _mapper;
    private readonly ILocationTypeQueryRepository _repository;
    private readonly IConfiguration _configuration;
    private readonly IDistributedRedisService _redisService;

    public LocationTypeQueryHandler(IMapper mapper, ILocationTypeQueryRepository repository, IConfiguration configuration, IDistributedRedisService redisService)
    {
        _mapper = mapper;
        _repository = repository;
        _configuration = configuration;
        _redisService = redisService;
    }

    public async Task<Result<GetLocationTypeQueryResult>> Handle(GetLocationTypeQuery request, CancellationToken cancellationToken)
    {
        var LocationType = await _repository.FindById(request.Id);
        var result = _mapper.Map<GetLocationTypeQueryResult>(LocationType);

        return Result.Ok(result);
    }

    public async Task<Result<List<GetLocationTypeQueryResult>>> Handle(GetAllLocationTypeQuery request, CancellationToken cancellationToken)
    {
        string appName = _configuration.GetSection("AppName").Value ?? "";
        string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.LocationType);
        bool redisResult = false;

        try
        {
            redisResult = _redisService.TryGet(redisKey, out List<GetLocationTypeQueryResult> values);
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