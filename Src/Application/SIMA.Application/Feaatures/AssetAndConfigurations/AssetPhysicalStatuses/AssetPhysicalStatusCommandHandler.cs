using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.AssetPhysicalStatuses;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.AssetPhysicalStatuses;

public class AssetPhysicalStatusCommandHandler : ICommandHandler<CreateAssetPhysicalStatusCommand, Result<long>>,
    ICommandHandler<ModifyAssetPhysicalStatusCommand, Result<long>>, ICommandHandler<DeleteAssetPhysicalStatusCommand, Result<long>>
{
    private readonly IAssetPhysicalStatusRepository _repository;
    private readonly IAssetPhysicalStatusDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;
    
    

    public AssetPhysicalStatusCommandHandler(IAssetPhysicalStatusRepository repository, IAssetPhysicalStatusDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateAssetPhysicalStatusCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateAssetPhysicalStatusArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await AssetPhysicalStatus.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyAssetPhysicalStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyAssetPhysicalStatusArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteAssetPhysicalStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}