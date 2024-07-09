using AutoMapper;
using Microsoft.Extensions.Configuration;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.AddressTypes;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Args;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Entities;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using SIMA.Framework.Infrastructure.Cachings;


namespace SIMA.Application.Feaatures.Auths.AddressTypes;

public class AddressTypeCommandHandler : ICommandHandler<DeleteAddressTypeCommand, Result<long>>, ICommandHandler<CreateAddressTypeCommand, Result<long>>,
    ICommandHandler<ModifyAddressTypeCommand, Result<long>>
{
    private readonly IAddressTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IDistributedRedisService _redisService;
    private readonly IConfiguration _configuration;
    private readonly IAddressTypeDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public AddressTypeCommandHandler(IAddressTypeRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IDistributedRedisService redisService, IConfiguration configuration,
        IAddressTypeDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _redisService = redisService;
        _configuration = configuration;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(DeleteAddressTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);

        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        //DeleteCachedData();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateAddressTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateAddressTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var addressType = await AddressType.Create(arg, _service);
        await _repository.Add(addressType);
        await _unitOfWork.SaveChangesAsync();
        //DeleteCachedData();
        return Result.Ok(addressType.Id.Value);

    }
    public async Task<Result<long>> Handle(ModifyAddressTypeCommand request, CancellationToken cancellationToken)
    {
        var addressType = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyAddressTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await addressType.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        //DeleteCachedData();
        return Result.Ok(addressType.Id.Value);
    }
    /// <summary>
    /// for reinitializing caches in first read
    /// </summary>
    private void DeleteCachedData()
    {
        string appName = _configuration.GetSection("AppName").Value ?? "";
        string redisKey = RedisHelper.GenerateRedisKey(appName, "basics", RedisKeys.AddressTypes);
        _redisService.Delete(redisKey);
    }
}
