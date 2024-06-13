using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Logistics.UnitMeasurements;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Args;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Contracts;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Entities;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Logistics.UnitMeasurements;

public class UnitMeasurementCommandHandler : ICommandHandler<CreateUnitMeasurementCommand, Result<long>>,
    ICommandHandler<ModifyUnitMeasurementCommand, Result<long>>, ICommandHandler<DeleteUnitMeasurementCommand, Result<long>>
{
    private readonly IUnitMeasurementRepository _repository;
    private readonly IUnitMeasurementDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public UnitMeasurementCommandHandler(IUnitMeasurementRepository repository, IUnitMeasurementDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateUnitMeasurementCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateUnitMeasurementArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await UnitMeasurement.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyUnitMeasurementCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new UnitMeasurementId(request.Id));
        var arg = _mapper.Map<ModifyUnitMeasurementArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteUnitMeasurementCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new UnitMeasurementId(request.Id));
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}