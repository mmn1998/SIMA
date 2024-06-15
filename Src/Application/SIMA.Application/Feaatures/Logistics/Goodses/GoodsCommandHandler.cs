using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Logistics.Goods;
using SIMA.Domain.Models.Features.Logistics.Goodses.Args;
using SIMA.Domain.Models.Features.Logistics.Goodses.Contracts;
using SIMA.Domain.Models.Features.Logistics.Goodses.Entities;
using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Logistics.Goodses;

public class GoodsCommandHandler : ICommandHandler<CreateGoodsCommand, Result<long>>,
    ICommandHandler<ModifyGoodsCommand, Result<long>>, ICommandHandler<DeleteGoodsCommand, Result<long>>
{
    private readonly IGoodsRepository _repository;
    private readonly IGoodsDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public GoodsCommandHandler(IGoodsRepository repository, IGoodsDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateGoodsCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateGoodsArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Goods.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyGoodsCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new GoodsId(request.Id));
        var arg = _mapper.Map<ModifyGoodsArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteGoodsCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new GoodsId(request.Id));
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}