using AutoMapper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.ConsequenceCategories;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Args;

namespace SIMA.Application.Feaatures.RiskManagers.ConsequenceCategories;

public class ConsequenceCategoryCommandHandler : ICommandHandler<CreateConsequenceCategoryCommand, Result<long>>, ICommandHandler<ModifyConsequenceCategoryCommand, Result<long>>
, ICommandHandler<DeleteConsequenceCategoryCommand, Result<long>>
{
    private readonly IConsequenceCategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IConsequenceCategoryDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public ConsequenceCategoryCommandHandler(IConsequenceCategoryRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IConsequenceCategoryDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateConsequenceCategoryCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateConsequenceCategoryArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ConsequenceCategory.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyConsequenceCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyConsequenceCategoryArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteConsequenceCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
