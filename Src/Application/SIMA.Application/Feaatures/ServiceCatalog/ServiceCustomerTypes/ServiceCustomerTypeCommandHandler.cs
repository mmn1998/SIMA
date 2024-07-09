using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceCustomerTypes;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServiceCustomerTypes;

public class ServiceCustomerTypeCommandHandler : ICommandHandler<CreateServiceCustomerTypeCommand, Result<long>>,
    ICommandHandler<ModifyServiceCustomerTypeCommand, Result<long>>, ICommandHandler<DeleteServiceCustomerTypeCommand, Result<long>>
{
    private readonly IServiceCustomerTypeRepository _repository;
    private readonly IServiceCustomerTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ServiceCustomerTypeCommandHandler(IServiceCustomerTypeRepository repository, IServiceCustomerTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateServiceCustomerTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateServiceCustomerTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ServiceCustomerType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyServiceCustomerTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ServiceCustomerTypeId(request.Id));
        var arg = _mapper.Map<ModifyServiceCustomerTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteServiceCustomerTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ServiceCustomerTypeId(request.Id));
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}