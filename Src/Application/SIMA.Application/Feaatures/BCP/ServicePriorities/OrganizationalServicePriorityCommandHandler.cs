using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.ServicePriorities;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.Args;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.Contracts;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.Entities;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.ServicePriorities;

public class OrganizationalServicePriorityCommandHandler : ICommandHandler<CreateOrganizationalServicePriorityCommand, Result<long>>,
    ICommandHandler<ModifyOrganizationalServicePriorityCommand, Result<long>>, ICommandHandler<DeleteOrganizationalServicePriorityCommand, Result<long>>
{
    private readonly IOrganizationalServicePriorityRepository _repository;
    private readonly IOrganizationalServicePriorityDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public OrganizationalServicePriorityCommandHandler(IOrganizationalServicePriorityRepository repository, IOrganizationalServicePriorityDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateOrganizationalServicePriorityCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateOrganizationalServicePriorityArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await OrganizationalServicePriority.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyOrganizationalServicePriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new OrganizationalServicePriorityId(request.Id));
        var arg = _mapper.Map<ModifyOrganizationalServicePriorityArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteOrganizationalServicePriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new OrganizationalServicePriorityId(request.Id));
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}