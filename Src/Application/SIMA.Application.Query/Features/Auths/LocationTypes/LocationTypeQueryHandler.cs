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
        string appName = _configuration.GetSection("AppName").Value ?? "";
        string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.LocationType);
        bool redisResult = false;

        try
        {

            redisResult = _redisService.TryGet(redisKey, out List<GetLocationTypeQueryResult> values);
            if (!redisResult) throw new Exception();
            values = string.IsNullOrEmpty(request.Filter) ? values : values
                .Where(it => it.Name.Contains(request.Filter) || it.Code.Contains(request.Filter)
                || it.ActiveStatus.Contains(request.Filter) || it.ParentName.Contains(request.Filter))
                .ToList();
            int totalCount = values.Count();
            values = values.Skip(request.Skip).Take(request.PageSize).ToList();
            return Result.Ok(values.AsEnumerable(), totalCount, request.PageSize, request.Page);
        }
        catch
        {
            return await _repository.GetAll(request);
        }
    }
}