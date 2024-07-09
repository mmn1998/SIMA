using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Logistics.GoodsCategories;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Args;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Logistics.GoodsCategories;

public class GoodsCategoryCommandHandler : ICommandHandler<CreateGoodsCategoryCommand, Result<long>>,
    ICommandHandler<ModifyGoodsCategoryCommand, Result<long>>, ICommandHandler<DeleteGoodsCategoryCommand, Result<long>>
{
    private readonly IGoodsCategoryRepository _repository;
    private readonly IGoodsCategoryDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public GoodsCategoryCommandHandler(IGoodsCategoryRepository repository, IGoodsCategoryDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateGoodsCategoryCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateGoodsCategoryArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await GoodsCategory.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyGoodsCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new GoodsCategoryId(request.Id));
        var arg = _mapper.Map<ModifyGoodsCategoryArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteGoodsCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new GoodsCategoryId(request.Id));
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}