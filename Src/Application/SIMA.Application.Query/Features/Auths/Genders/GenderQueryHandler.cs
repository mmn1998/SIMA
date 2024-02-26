using AutoMapper;
using Microsoft.Extensions.Configuration;
using SIMA.Application.Query.Contract.Features.Auths.Gender;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Persistance.Read.Repositories.Features.Auths.Genders;

namespace SIMA.Application.Query.Features.Auths.Genders;

public class GenderQueryHandler : IQueryHandler<GetGenderQuery, Result<GetGenderQueryResult>>, IQueryHandler<GetAllGenderQuery, Result<List<GetGenderQueryResult>>>
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
        //var result = _mapper.Map<GetGenderQueryResult>(entity);
        return Result.Ok(result);
    }

    public async Task<Result<List<GetGenderQueryResult>>> Handle(GetAllGenderQuery request, CancellationToken cancellationToken)
    {
        string appName = _configuration.GetSection("AppName").Value ?? "";
        string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.Genders);
        bool redisResult = false;

        try
        {
            redisResult = _redisService.TryGet(redisKey, out List<GetGenderQueryResult> values);
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