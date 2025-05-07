using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemTypes;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.ConfigurationItemTypes;

public class ConfigurationItemTypeCommandHandler : ICommandHandler<CreateConfigurationItemTypeCommand, Result<long>>,
    ICommandHandler<ModifyConfigurationItemTypeCommand, Result<long>>, ICommandHandler<DeleteConfigurationItemTypeCommand, Result<long>>
{
    private readonly IConfigurationItemTypeRepository _repository;
    private readonly IConfigurationItemTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ConfigurationItemTypeCommandHandler(IConfigurationItemTypeRepository repository, IConfigurationItemTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateConfigurationItemTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateConfigurationItemTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ConfigurationItemType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyConfigurationItemTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyConfigurationItemTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteConfigurationItemTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}