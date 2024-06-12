using AutoMapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.LocationTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Persistance.Read.Repositories.Features.Auths.LocationTypes;

namespace SIMA.Application.Query.Features.Auths.LocationTypes;

public class LocationTypeQueryHandler : IQueryHandler<GetLocationTypeQuery, Result<GetLocationTypeQueryResult>>, IQueryHandler<GetAllLocationTypeQuery, Result<IEnumerable<GetLocationTypeQueryResult>>>
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

    public async Task<Result<IEnumerable<GetLocationTypeQueryResult>>> Handle(GetAllLocationTypeQuery request, CancellationToken cancellationToken)
    {
        #region Redis
        //string appName = _configuration.GetSection("AppName").Value ?? "";
        //string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.LocationType);
        //bool redisResult = false;

        //try
        //{
        //    redisResult = _redisService.TryGet(redisKey, out List<GetLocationTypeQueryResult> values);
        //    if (!redisResult) throw new Exception();
        //    if (request.Filters != null && request.Filters.Count != 0)
        //    {

        //        if (request.Filters.Any(x => x.Key == nameof(GetLocationTypeQueryResult.Name)))
        //        {
        //            var name = request.Filters.First(x => x.Key == nameof(GetLocationTypeQueryResult.Name)).Value;
        //            values = values.Where(x => x.Name.Contains(name)).ToList();
        //        }
        //        if (request.Filters.Any(x => x.Key == nameof(GetLocationTypeQueryResult.Code)))
        //        {
        //            var code = request.Filters.First(x => x.Key == nameof(GetLocationTypeQueryResult.Code)).Value;
        //            values = values.Where(x => x.Code.Contains(code)).ToList();
        //        }
        //        if (request.Filters.Any(x => x.Key == nameof(GetLocationTypeQueryResult.ParentName)))
        //        {
        //            var parentName = request.Filters.First(x => x.Key == nameof(GetLocationTypeQueryResult.ParentName)).Value;
        //            values = values.Where(x => x.ParentName.Contains(parentName)).ToList();
        //        }
        //    }
        //    int totalCount = values.Count();
        //    values = values.Skip(request.Skip).Take(request.PageSize).ToList();
        //    return Result.Ok(values.AsEnumerable(), totalCount, request.PageSize, request.Page);
        //}
        //catch
        //{
        //    return await _repository.GetAll(request);
        //}
        #endregion
        return await _repository.GetAll(request);
    }
}