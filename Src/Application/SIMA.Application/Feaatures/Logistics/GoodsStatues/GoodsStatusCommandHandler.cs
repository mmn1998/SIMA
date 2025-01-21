using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Logistics.GoodsStatues;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Args;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Logistics.GoodsStatues;

public class GoodsStatusCommandHandler : ICommandHandler<CreateGoodsStatusCommand, Result<long>>,
    ICommandHandler<ModifyGoodsStatusCommand, Result<long>>, ICommandHandler<DeleteGoodsStatusCommand, Result<long>>
{
    private readonly IGoodsStatusRepository _repository;
    private readonly IGoodsStatusDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public GoodsStatusCommandHandler(IGoodsStatusRepository repository, IGoodsStatusDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateGoodsStatusCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateGoodsStatusArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await GoodsStatus.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyGoodsStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyGoodsStatusArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteGoodsStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}