using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.AssetTechnicalStatuses;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.AssetTechnicalStatuses;

public class AssetTechnicalStatusCommandHandler : ICommandHandler<CreateAssetTechnicalStatusCommand, Result<long>>,
    ICommandHandler<ModifyAssetTechnicalStatusCommand, Result<long>>, ICommandHandler<DeleteAssetTechnicalStatusCommand, Result<long>>
{
    private readonly IAssetTechnicalStatusRepository _repository;
    private readonly IAssetTechnicalStatusDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public AssetTechnicalStatusCommandHandler(IAssetTechnicalStatusRepository repository, IAssetTechnicalStatusDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateAssetTechnicalStatusCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateAssetTechnicalStatusArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await AssetTechnicalStatus.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyAssetTechnicalStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyAssetTechnicalStatusArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteAssetTechnicalStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}