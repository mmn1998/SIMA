using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Gender;
using SIMA.Application.Query.Contract.Features.Auths.Locations;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Persistance.Read.Repositories.Features.Auths.Locations;

namespace SIMA.Application.Query.Features.Auths.Locations;

public class LocationQueryHandler : IQueryHandler<GetLocationQuery, Result<GetLocationQueryResult>>, IQueryHandler<GetAllLocationQuery, Result<IEnumerable<GetLocationQueryResult>>>
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
        var result = await _repository.FindById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetLocationQueryResult>>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
    {
        string appName = _configuration.GetSection("AppName").Value ?? "";
        string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.Location);
        bool redisResult = false;

        try
        {
            redisResult = _redisService.TryGet(redisKey, out List<GetLocationQueryResult> values);
            if (!redisResult) throw new Exception();
            if (request.Filters != null && request.Filters.Count != 0)
            {
                if (request.Filters.Any(x => x.Key == nameof(GetLocationQueryResult.LocationName)))
                {
                    var name = request.Filters.First(x => x.Key == nameof(GetLocationQueryResult.LocationName)).Value;
                    values = values.Where(x => x.LocationName.Contains(name)).ToList();
                }
                if (request.Filters.Any(x => x.Key == nameof(GetLocationQueryResult.LocationTypeName)))
                {
                    var code = request.Filters.First(x => x.Key == nameof(GetLocationQueryResult.LocationTypeName)).Value;
                    values = values.Where(x => x.LocationTypeName.Contains(code)).ToList();
                }
                if (request.Filters.Any(x => x.Key == nameof(GetLocationQueryResult.ParentLocationTypeName)))
                {
                    var code = request.Filters.First(x => x.Key == nameof(GetLocationQueryResult.ParentLocationTypeName)).Value;
                    values = values.Where(x => x.ParentLocationTypeName.Contains(code)).ToList();
                }
            }
            int totalCount = values.Count();
            values = values.Skip(request.Skip).Take(request.PageSize).ToList();
            return Result.Ok(values.AsEnumerable(), totalCount, request.PageSize, request.Page);
        }
        catch
        {
            return await _repository.GetAll(request);
        }
    }

    public async Task<Result<List<GetParentLocationsByLocationTypeIdQueryResult>>> Handle(GetParentLocationsByLocationTypeIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _repository.GetParentsByChildId(request.LocationTypeId);
        return Result.Ok(response);
    }
}