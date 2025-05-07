using AutoMapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.AddressTypes;
using SIMA.Application.Query.Contract.Features.Auths.Gender;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Persistance.Read.Repositories.Features.Auths.Genders;

namespace SIMA.Application.Query.Features.Auths.Genders;

public class GenderQueryHandler : IQueryHandler<GetGenderQuery, Result<GetGenderQueryResult>>, IQueryHandler<GetAllGenderQuery, Result<IEnumerable<GetGenderQueryResult>>>
{
    private readonly IGenderQueryRepository _repository;
    private readonly IConfiguration _configuration;
    private readonly IDistributedRedisService _redisService;

    public GenderQueryHandler(IGenderQueryRepository repository, IConfiguration configuration, IDistributedRedisService redisService)
    {
        _repository = repository;
        _configuration = configuration;
        _redisService = redisService;
    }

    public async Task<Result<GetGenderQueryResult>> Handle(GetGenderQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.FindById(request.Id);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetGenderQueryResult>>> Handle(GetAllGenderQuery request, CancellationToken cancellationToken)
    {
        #region Redis
        //string appName = _configuration.GetSection("AppName").Value ?? "";
        //string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.Genders);
        //bool redisResult = false;

        //try
        //{

        //    redisResult = _redisService.TryGet(redisKey, out List<GetGenderQueryResult> values);
        //    if (!redisResult) throw new Exception();
        //    if (request.Filters.Any(x => x.Key == nameof(GetGenderQueryResult.Name)))
        //    {
        //        var name = request.Filters.First(x => x.Key == nameof(GetGenderQueryResult.Name)).Value;
        //        values = values.Where(x => x.Name.Contains(name)).ToList();
        //    }
        //    if (request.Filters.Any(x => x.Key == nameof(GetGenderQueryResult.Code)))
        //    {
        //        var code = request.Filters.First(x => x.Key == nameof(GetGenderQueryResult.Code)).Value;
        //        values = values.Where(x => x.Code.Contains(code)).ToList();
        //    }

        //    int totalCount = values.Count;
        //    values = values.Skip(request.Skip).Take(request.PageSize).ToList();
        //    return Result.Ok(values.AsEnumerable(), request, totalCount);
        //}
        //catch
        //{
        //    return await _repository.GetAll(request);
        //}
        #endregion
        return await _repository.GetAll(request);
    }
}