using AutoMapper;
using Microsoft.Extensions.Configuration;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.LocationTypes;
using SIMA.Domain.Models.Features.Auths.LocationTypes;
using SIMA.Domain.Models.Features.Auths.LocationTypes.Args;
using SIMA.Domain.Models.Features.Auths.LocationTypes.Entities;
using SIMA.Domain.Models.Features.Auths.LocationTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Framework.Infrastructure.Cachings;

namespace SIMA.Application.Feaatures.Auths.LocationTypes;

public class LocationTypeCommandHandler : ICommandHandler<CreateLocationTypeCommand, Result<long>>, ICommandHandler<DeleteLocationTypeCommand, Result<long>>,
    ICommandHandler<ModifyLocationTypeCommand, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly ILocationTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDistributedRedisService _redisService;
    private readonly IConfiguration _configuration;
    private readonly ILocationTypeService _service;

    public LocationTypeCommandHandler(IMapper mapper, ILocationTypeRepository repository, IUnitOfWork unitOfWork,
        IDistributedRedisService redisService, IConfiguration configuration, ILocationTypeService service)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _redisService = redisService;
        _configuration = configuration;
        _service = service;
    }
    public async Task<Result<long>> Handle(CreateLocationTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateLocationTypeArg>(request);
        var entity = await LocationType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        DeleteCachedData();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(ModifyLocationTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById((int)request.Id);
        var arg = _mapper.Map<ModifyLocationTypeArg>(request);
        entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        DeleteCachedData();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeleteLocationTypeCommand request, CancellationToken cancellationToken)
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
        string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.LocationType);
        _redisService.Delete(redisKey);
    }
}
