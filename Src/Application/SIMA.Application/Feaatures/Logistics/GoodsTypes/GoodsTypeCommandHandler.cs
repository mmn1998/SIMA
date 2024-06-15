using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Logistics.GoodsTypes;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Args;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Logistics.GoodsTypes;

public class GoodsTypeCommandHandler : ICommandHandler<CreateGoodsTypeCommand, Result<long>>,
    ICommandHandler<ModifyGoodsTypeCommand, Result<long>>, ICommandHandler<DeleteGoodsTypeCommand, Result<long>>
{
    private readonly IGoodsTypeRepository _repository;
    private readonly IGoodsTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public GoodsTypeCommandHandler(IGoodsTypeRepository repository, IGoodsTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateGoodsTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateGoodsTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await GoodsType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyGoodsTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new GoodsTypeId(request.Id));
        var arg = _mapper.Map<ModifyGoodsTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteGoodsTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new GoodsTypeId(request.Id));
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}