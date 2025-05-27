using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Warehouses;
using SIMA.Domain.Models.Features.Auths.Warehouses.Args;
using SIMA.Domain.Models.Features.Auths.Warehouses.Contracts;
using SIMA.Domain.Models.Features.Auths.Warehouses.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.Warehouses;

public class WarehouseCommandHandler : ICommandHandler<CreateWarehouseCommand, Result<long>>,
    ICommandHandler<ModifyWarehouseCommand, Result<long>>, ICommandHandler<DeleteWarehouseCommand, Result<long>>
{
    private readonly IWarehouseRepository _repository;
    private readonly IWarehouseDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public WarehouseCommandHandler(IWarehouseRepository repository, IWarehouseDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateWarehouseArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Warehouse.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyWarehouseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyWarehouseArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}