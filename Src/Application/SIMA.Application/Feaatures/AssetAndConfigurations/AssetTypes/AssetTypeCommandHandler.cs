using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.AssetTypes;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.AssetTypes;

public class AssetTypeCommandHandler : ICommandHandler<CreateAssetTypeCommand, Result<long>>,
    ICommandHandler<ModifyAssetTypeCommand, Result<long>>, ICommandHandler<DeleteAssetTypeCommand, Result<long>>
{
    private readonly IAssetTypeRepository _repository;
    private readonly IAssetTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public AssetTypeCommandHandler(IAssetTypeRepository repository, IAssetTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateAssetTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateAssetTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await AssetType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyAssetTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyAssetTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteAssetTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}