using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.EvaluationCriterias;
using SIMA.Application.Contract.Features.RiskManagers.ScenarioHistories;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Args;
using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Contracts;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;

namespace SIMA.Application.Feaatures.RiskManagers.ScenarioHistories;

public class ScenarioHistoryCommandHandler: ICommandHandler<CreateScenarioHistoryCommand, Result<long>>,
    ICommandHandler<ModifyScenarioHistoryCommand, Result<long>>, ICommandHandler<DeleteScenarioHistoryCommand, Result<long>>
{
    
    private readonly IScenarioHistoryRepository _repository;
    private readonly IScenarioHistoryDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ScenarioHistoryCommandHandler(IScenarioHistoryRepository repository, IScenarioHistoryDomainService service, IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }


    public async Task<Result<long>> Handle(CreateScenarioHistoryCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateScenarioHistoryArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ScenarioHistory.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyScenarioHistoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyScenarioHistoryArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteScenarioHistoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}