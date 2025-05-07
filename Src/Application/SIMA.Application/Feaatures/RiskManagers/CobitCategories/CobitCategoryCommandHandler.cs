using AutoMapper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.CobitCategories;
using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.Args;
using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.Entities;

namespace SIMA.Application.Feaatures.RiskManagers.CobitCategories;

public class CobitCategoryCommandHandler : ICommandHandler<CreateCobitCategoryCommand, Result<long>>, ICommandHandler<ModifyCobitCategoryCommand, Result<long>>
, ICommandHandler<DeleteCobitCategoryCommand, Result<long>>
{
    private readonly ICobitCategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICobitCategoryDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public CobitCategoryCommandHandler(ICobitCategoryRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, ICobitCategoryDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateCobitCategoryCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateCobitCategoryArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await CobitCategory.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyCobitCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyCobitCategoryArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteCobitCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}