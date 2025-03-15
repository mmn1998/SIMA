using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Assets;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.Assets;

public class AssetCommandHandler : ICommandHandler<CreateAssetCommand, Result<long>>, ICommandHandler<ModifyAssetCommand, Result<long>>,
    ICommandHandler<DeleteAssetCommand, Result<long>>
{
    private readonly IAssetRepository _repository;
    private readonly IAssetDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public AssetCommandHandler(IAssetRepository repository, IAssetDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }

    public async Task<Result<long>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateAssetArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Asset.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyAssetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyAssetArg>(request);
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var userId = _simaIdentity.UserId;
        entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

}
