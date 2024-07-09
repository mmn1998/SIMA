using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Logistics.Suppliers;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Args;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Contracts;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Entities;
using SIMA.Domain.Models.Features.Logistics.Suppliers.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Logistics.Suppliers;

public class SupplierCommandHandler : ICommandHandler<CreateSupplierCommand, Result<long>>,
    ICommandHandler<ModifySupplierCommand, Result<long>>, ICommandHandler<DeleteSupplierCommand, Result<long>>
{
    private readonly ISupplierRepository _repository;
    private readonly ISupplierDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public SupplierCommandHandler(ISupplierRepository repository, ISupplierDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateSupplierArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Supplier.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifySupplierCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new SupplierId(request.Id));
        var arg = _mapper.Map<ModifySupplierArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new SupplierId(request.Id));
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}