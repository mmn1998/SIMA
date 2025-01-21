using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.ServicePriorities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriorities.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriorities.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriorities.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServicePriorities;

public class ServicePriorityCommandHandler : 
    ICommandHandler<CreateServicePriorityCommand, Result<long>>,
    ICommandHandler<DeleteServicePriorityCommand, Result<long>>,
    ICommandHandler<ModifyServicePriorityCommand, Result<long>>

{
    private readonly IServicePriorityRepository _repository;
    private readonly IServicePriorityDomainService _ServicePriorityDomainService;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ServicePriorityCommandHandler(IServicePriorityRepository repository, IUnitOfWork unitOfWork, IMapper mapper,
        IServicePriorityDomainService ServicePriorityDomainService, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ServicePriorityDomainService = ServicePriorityDomainService;
        _simaIdentity = simaIdentity;
    }

    public async Task<Result<long>> Handle(CreateServicePriorityCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateServicePriorityArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ServicePriority.Create(arg, _ServicePriorityDomainService);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(ModifyServicePriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ServicePriorityId(request.Id));
        var arg = _mapper.Map<ModifyServicePriorityArgs>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _ServicePriorityDomainService);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    public async Task<Result<long>> Handle(DeleteServicePriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ServicePriorityId(request.Id));
        long userId = _simaIdentity.UserId;
        entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
