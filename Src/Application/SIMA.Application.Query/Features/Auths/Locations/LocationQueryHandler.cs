using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Locations;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Persistance.Read.Repositories.Features.Auths.Locations;

namespace SIMA.Application.Query.Features.Auths.Locations;

public class LocationQueryHandler : IQueryHandler<GetLocationQuery, Result<GetLocationQueryResult>>, IQueryHandler<GetAllLocationQuery, Result<List<GetLocationQueryResult>>>
    , IQueryHandler<GetParentLocationsByLocationTypeIdQuery, Result<List<GetParentLocationsByLocationTypeIdQueryResult>>>
{
    private readonly ILocationQueryRepository _repository;
    private readonly IConfiguration _configuration;
    private readonly IDistributedRedisService _redisService;

    public LocationQueryHandler(ILocationQueryRepository repository, IConfiguration configuration, IDistributedRedisService redisService)
    {
        _repository = repository;
        _configuration = configuration;
        _redisService = redisService;
    }

    public async Task<Result<GetLocationQueryResult>> Handle(GetLocationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.FindById(request.Id);
            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public async Task<Result<List<GetLocationQueryResult>>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
    {
        string appName = _configuration.GetSection("AppName").Value ?? "";
        string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.Location);
        bool redisResult = false;

        try
        {
            redisResult = _redisService.TryGet(redisKey, out List<GetLocationQueryResult> values);
            if (!redisResult) throw new Exception();

            values = string.IsNullOrEmpty(request.Request.SearchValue) ? values : values.Where(it => it.LocationName.Contains(request.Request.SearchValue) || it.LocationCode.Contains(request.Request.SearchValue) || it.LocationTypeName.Contains(request.Request.SearchValue)).ToList();
            int totalCount = values.Count();
            values = values.Skip((request.Request.Skip - 1) * request.Request.Take).Take(request.Request.Take).ToList();
            return Result.Ok(values, totalCount);

        }
        catch (Exception e)
        {
            return await _repository.GetAll(request.Request);
        }

    }

    public async Task<Result<List<GetParentLocationsByLocationTypeIdQueryResult>>> Handle(GetParentLocationsByLocationTypeIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _repository.GetParentsByChildId(request.LocationTypeId);
        return Result.Ok(response);
    }
}