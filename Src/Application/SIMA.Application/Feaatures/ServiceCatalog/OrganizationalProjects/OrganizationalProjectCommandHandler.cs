using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.OrganizationalProjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.OrganizationalProjects;

public class OrganizationalProjectCommandHandler : ICommandHandler<CreateOrganizationalProjectCommand, Result<long>>,
ICommandHandler<ModifyOrganizationalProjectCommand, Result<long>>, ICommandHandler<DeleteOrganizationalProjectCommand, Result<long>>
{
    private readonly IOrganizationalProjectRepository _repository;
    private readonly IOrganizationalProjectDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public OrganizationalProjectCommandHandler(IOrganizationalProjectRepository repository, IOrganizationalProjectDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateOrganizationalProjectCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateOrganizationalProjectArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await OrganizationalProject.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyOrganizationalProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new OrganizationalProjectId(request.Id));
        var arg = _mapper.Map<ModifyOrganizationalProjectArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteOrganizationalProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new OrganizationalProjectId(request.Id));
        long userId = _simaIdentity.UserId;
        entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
