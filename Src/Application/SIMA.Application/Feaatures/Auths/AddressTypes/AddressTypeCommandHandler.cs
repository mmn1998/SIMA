using AutoMapper;
using Microsoft.Extensions.Configuration;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.AddressTypes;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Args;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Entities;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Interfaces;
using SIMA.Framework.Common.Response;
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

    public AddressTypeCommandHandler(IAddressTypeRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IDistributedRedisService redisService, IConfiguration configuration,
        IAddressTypeDomainService service)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _redisService = redisService;
        _configuration = configuration;
        _service = service;
    }
    public async Task<Result<long>> Handle(DeleteAddressTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById((int)request.Id);

        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        DeleteCachedData();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateAddressTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var arg = _mapper.Map<CreateAddressTypeArg>(request);
            var addressType = await AddressType.Create(arg, _service);
            await _repository.Add(addressType);
            await _unitOfWork.SaveChangesAsync();
            DeleteCachedData();
            return Result.Ok(addressType.Id.Value);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    public async Task<Result<long>> Handle(ModifyAddressTypeCommand request, CancellationToken cancellationToken)
    {
        var addressType = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyAddressTypeArg>(request);
        addressType.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        DeleteCachedData();
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
