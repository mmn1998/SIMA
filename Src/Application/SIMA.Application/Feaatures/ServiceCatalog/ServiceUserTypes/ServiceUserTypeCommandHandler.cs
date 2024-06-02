using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceUserTypes;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceUserTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServiceUserTypes;

public class ServiceUserTypeCommandHandler : ICommandHandler<CreateServiceUserTypeCommand, Result<long>>,
    ICommandHandler<ModifyServiceUserTypeCommand, Result<long>>, ICommandHandler<DeleteServiceUserTypeCommand, Result<long>>
{
    private readonly IServiceUserTypeRepository _repository;
    private readonly IServiceUserTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ServiceUserTypeCommandHandler(IServiceUserTypeRepository repository, IServiceUserTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateServiceUserTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateServiceUserTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ServiceUserType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyServiceUserTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ServiceUserTypeId(request.Id));
        var arg = _mapper.Map<ModifyServiceUserTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteServiceUserTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ServiceUserTypeId(request.Id));
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}