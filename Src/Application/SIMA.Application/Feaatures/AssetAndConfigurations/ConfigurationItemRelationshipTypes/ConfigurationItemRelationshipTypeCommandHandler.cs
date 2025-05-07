using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemRelationshipTypes;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.ConfigurationItemRelationshipTypes;

public class ConfigurationItemRelationshipTypeCommandHandler : ICommandHandler<CreateConfigurationItemRelationshipTypeCommand, Result<long>>,
    ICommandHandler<ModifyConfigurationItemRelationshipTypeCommand, Result<long>>, ICommandHandler<DeleteConfigurationItemRelationshipTypeCommand, Result<long>>
{
    private readonly IConfigurationItemRelationshipTypeRepository _repository;
    private readonly IConfigurationItemRelationshipTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ConfigurationItemRelationshipTypeCommandHandler(IConfigurationItemRelationshipTypeRepository repository, IConfigurationItemRelationshipTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateConfigurationItemRelationshipTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateConfigurationItemRelationshipTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ConfigurationItemRelationshipType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyConfigurationItemRelationshipTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyConfigurationItemRelationshipTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteConfigurationItemRelationshipTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}