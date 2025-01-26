using AutoMapper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Args;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Contracts;
using SIMA.Application.Contract.Features.RiskManagers.ConsequenceLevels;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Args;

namespace SIMA.Application.Feaatures.RiskManagers.ConsequenceLevels;

public class ConsequenceLevelCommandHandler : ICommandHandler<CreateConsequenceLevelCommand, Result<long>>, ICommandHandler<ModifyConsequenceLevelCommand, Result<long>>
, ICommandHandler<DeleteConsequenceLevelCommand, Result<long>>
{
    private readonly IConsequenceLevelRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IConsequenceLevelDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public ConsequenceLevelCommandHandler(IConsequenceLevelRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IConsequenceLevelDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateConsequenceLevelCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateConsequenceLevelArg>(request);
        var userId = _simaIdentity.UserId;
        arg.CreatedBy = userId;
        var entity = await ConsequenceLevel.Create(arg, _service);
        if (request.ConsequenceCategoryList is not null && request.ConsequenceCategoryList.Count > 0)
        {
            var categoryArgs = _mapper.Map<List<CreateConsequenceLevelCategoryArg>>(request.ConsequenceCategoryList);
            foreach (var categoryArg in categoryArgs)
            {
                categoryArg.ConsequenceLevelId = arg.Id;
                categoryArg.CreatedBy = userId;
            }
            entity.AddCategories(categoryArgs);
        }
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyConsequenceLevelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyConsequenceLevelArg>(request);
        var userId = _simaIdentity.UserId;
        arg.ModifiedBy = userId;
        await entity.Modify(arg, _service);
        if (request.ConsequenceCategoryList is not null && request.ConsequenceCategoryList.Count > 0)
        {
            var categoryArgs = _mapper.Map<List<CreateConsequenceLevelCategoryArg>>(request.ConsequenceCategoryList);
            foreach (var categoryArg in categoryArgs)
            {
                categoryArg.ConsequenceLevelId = request.Id;
                categoryArg.CreatedBy = userId;
            }
            entity.ModifyCategories(categoryArgs);
        }
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteConsequenceLevelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
