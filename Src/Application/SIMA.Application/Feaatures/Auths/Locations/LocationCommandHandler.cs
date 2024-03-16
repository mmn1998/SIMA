using AutoMapper;
using Microsoft.Extensions.Configuration;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Locations;
using SIMA.Domain.Models.Features.Auths.Locations;
using SIMA.Domain.Models.Features.Auths.Locations.Args;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Framework.Infrastructure.Cachings;

namespace SIMA.Application.Feaatures.Auths.Locations;

public class LocationCommandHandler : ICommandHandler<CreateLocationCommand, Result<long>>, ICommandHandler<DeleteLocationCommand, Result<long>>, ICommandHandler<ModifyLocationCommand, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly ILocationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDistributedRedisService _redisService;
    private readonly IConfiguration _configuration;
    private readonly ILocationService _service;

    public LocationCommandHandler(IMapper mapper, ILocationRepository repository, IUnitOfWork unitOfWork,
        IDistributedRedisService redisService, IConfiguration configuration, ILocationService service)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _redisService = redisService;
        _configuration = configuration;
        _service = service;
    }
    public async Task<Result<long>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateLocationArg>(request);
        var entity = await Location.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        DeleteCachedData();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(ModifyLocationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById((int)request.Id);
        var arg = _mapper.Map<ModifyLocationArg>(request);
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        DeleteCachedData();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById((int)request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        DeleteCachedData();
        return Result.Ok(entity.Id.Value);
    }
    /// <summary>
    /// for reinitializing caches in first read
    /// </summary>
    private void DeleteCachedData()
    {
        string appName = _configuration.GetSection("AppName").Value ?? "";
        string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.Location);
        _redisService.Delete(redisKey);
    }
}
