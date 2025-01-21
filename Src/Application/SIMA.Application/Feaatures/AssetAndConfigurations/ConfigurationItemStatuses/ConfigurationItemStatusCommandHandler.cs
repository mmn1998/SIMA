using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemStatuses;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.ConfigurationItemStatuses;

public class ConfigurationItemStatusCommandHandler : ICommandHandler<CreateConfigurationItemStatusCommand, Result<long>>,
    ICommandHandler<ModifyConfigurationItemStatusCommand, Result<long>>, ICommandHandler<DeleteConfigurationItemStatusCommand, Result<long>>
{
    private readonly IConfigurationItemStatusRepository _repository;
    private readonly IConfigurationItemStatusDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ConfigurationItemStatusCommandHandler(IConfigurationItemStatusRepository repository, IConfigurationItemStatusDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateConfigurationItemStatusCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateConfigurationItemStatusArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ConfigurationItemStatus.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyConfigurationItemStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyConfigurationItemStatusArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteConfigurationItemStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}