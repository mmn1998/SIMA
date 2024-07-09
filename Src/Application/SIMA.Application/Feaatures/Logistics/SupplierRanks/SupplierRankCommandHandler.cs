using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Logistics.SupplierRanks;
using SIMA.Domain.Models.Features.Logistics.SupplierRanks.Args;
using SIMA.Domain.Models.Features.Logistics.SupplierRanks.Entities;
using SIMA.Domain.Models.Features.Logistics.SupplierRanks.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Contracts;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Logistics.SupplierRanks;

public class SupplierRankCommandHandler : ICommandHandler<CreateSupplierRankCommand, Result<long>>,
    ICommandHandler<ModifySupplierRankCommand, Result<long>>, ICommandHandler<DeleteSupplierRankCommand, Result<long>>
{
    private readonly ISupplierRankRepository _repository;
    private readonly ISupplierRankDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public SupplierRankCommandHandler(ISupplierRankRepository repository, ISupplierRankDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateSupplierRankCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateSupplierRankArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await SupplierRank.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifySupplierRankCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new SupplierRankId(request.Id));
        var arg = _mapper.Map<ModifySupplierRankArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteSupplierRankCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new SupplierRankId(request.Id));
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}