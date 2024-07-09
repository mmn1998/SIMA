using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceTypes;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServiceTypes;

public class ServiceTypeCommandHandler : ICommandHandler<CreateServiceTypeCommand, Result<long>>,
    ICommandHandler<ModifyServiceTypeCommand, Result<long>>, ICommandHandler<DeleteServiceTypeCommand, Result<long>>
{
    private readonly IServiceTypeRepository _repository;
    private readonly IServiceTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ServiceTypeCommandHandler(IServiceTypeRepository repository, IServiceTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateServiceTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateServiceTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ServiceType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyServiceTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ServiceTypeId(request.Id));
        var arg = _mapper.Map<ModifyServiceTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteServiceTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ServiceTypeId(request.Id));
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}