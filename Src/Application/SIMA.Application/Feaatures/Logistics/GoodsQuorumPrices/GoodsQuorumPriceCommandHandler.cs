using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Logistics.GoodsQuorumPrices;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Args;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Contracts;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Logistics.GoodsQuorumPrices;

public class GoodsQuorumPriceCommandHandler : ICommandHandler<CreateGoodsQuorumPriceCommand, Result<long>>,
    ICommandHandler<ModifyGoodsQuorumPriceCommand, Result<long>>, ICommandHandler<DeleteGoodsQuorumPriceCommand, Result<long>>
{
    private readonly IGoodsQuorumPriceRepository _repository;
    private readonly IGoodsQuorumPriceDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public GoodsQuorumPriceCommandHandler(IGoodsQuorumPriceRepository repository, IGoodsQuorumPriceDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateGoodsQuorumPriceCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateGoodsQuorumPriceArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await GoodsQuorumPrice.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyGoodsQuorumPriceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new GoodsQuorumPriceId(request.Id));
        var arg = _mapper.Map<ModifyGoodsQuorumPriceArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteGoodsQuorumPriceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new GoodsQuorumPriceId(request.Id));
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}