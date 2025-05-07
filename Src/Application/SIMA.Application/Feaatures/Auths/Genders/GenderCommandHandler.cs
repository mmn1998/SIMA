using AutoMapper;
using Microsoft.Extensions.Configuration;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Genders;
using SIMA.Domain.Models.Features.Auths.Genders;
using SIMA.Domain.Models.Features.Auths.Genders.Args;
using SIMA.Domain.Models.Features.Auths.Genders.Entities;
using SIMA.Domain.Models.Features.Auths.Genders.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using SIMA.Framework.Infrastructure.Cachings;

namespace SIMA.Application.Feaatures.Auths.Genders;

public class GenderCommandHandler : ICommandHandler<CreateGenderCommand, Result<long>>,
    ICommandHandler<DeleteGenderCommand, Result<long>>, ICommandHandler<ModifyGenderCommand, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly IGenderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDistributedRedisService _redisService;
    private readonly IConfiguration _configuration;
    private readonly IGenderService _service;
    private readonly ISimaIdentity _simaIdentity;

    public GenderCommandHandler(IMapper mapper, IGenderRepository repository,
        IUnitOfWork unitOfWork, IDistributedRedisService redisService, IConfiguration configuration,
        IGenderService service, ISimaIdentity simaIdentity)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _redisService = redisService;
        _configuration = configuration;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateGenderCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateGenderArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Gender.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        //DeleteCachedData();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(ModifyGenderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyGenderArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        //DeleteCachedData();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeleteGenderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        //DeleteCachedData();
        return Result.Ok(entity.Id.Value);
    }
    /// <summary>
    /// for reinitializing caches in first read
    /// </summary>
    private void DeleteCachedData()
    {
        string appName = _configuration.GetSection("AppName").Value ?? "";
        string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.Genders);
        _redisService.Delete(redisKey);
    }
}
