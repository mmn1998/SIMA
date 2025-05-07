using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.Senarios;
using SIMA.Domain.Models.Features.BCP.Scenarios.Args;
using SIMA.Domain.Models.Features.BCP.Scenarios.Contracts;
using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.Scenarios;

public class ScenarioCommandHandler : ICommandHandler<CreateSenarioCommand, Result<long>>,
ICommandHandler<ModifySenarioCommand, Result<long>>, ICommandHandler<DeleteScenarioCommand, Result<long>>
{
    private readonly IScenarioRepisitory _repository;
    private readonly IScenarioDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ScenarioCommandHandler(IScenarioRepisitory repository, IScenarioDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateSenarioCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateScenarioArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Scenario.Create(arg, _service);

        //if(request.ScenarioBusinessContinuityPlanVersionings is not null && request.ScenarioBusinessContinuityPlanVersionings.Count() > 0)
        //{
        //    var scenarioBusinessContinuityPlanVersioningArg = _mapper.Map<List<CreateScenarioBusinessContinuityPlanVersioningArg>>(request.ScenarioBusinessContinuityPlanVersionings);
        //    foreach (var permission in scenarioBusinessContinuityPlanVersioningArg)
        //    {
        //        permission.CreatedBy = _simaIdentity.UserId;
        //    }
        //    await entity.AddScenarioBusinessContinuityPlanVersioning(scenarioBusinessContinuityPlanVersioningArg, entity.Id.Value);
        //}

        //if (request.ScenarioBusinessContinuityPlanAssumptions is not null && request.ScenarioBusinessContinuityPlanAssumptions.Count() > 0)
        //{
        //    var scenarioBusinessContinuityPlanAssumptionsArg = _mapper.Map<List<CreateScenarioBusinessContinuityPlanAssumptionArg>>(request.ScenarioBusinessContinuityPlanAssumptions);
        //    foreach (var permission in scenarioBusinessContinuityPlanAssumptionsArg)
        //    {
        //        permission.CreatedBy = _simaIdentity.UserId;
        //    }
        //    await entity.AddScenarioBusinessContinuityPlanAssumption(scenarioBusinessContinuityPlanAssumptionsArg, entity.Id.Value);
        //}

        if (request.ScenarioPlanRecoveryCriterias is not null && request.ScenarioPlanRecoveryCriterias.Count() > 0)
        {
            var scenarioPlanRecoveryCriteriasArg = _mapper.Map<List<CreateScenarioRecoveryCriteriaArg>>(request.ScenarioPlanRecoveryCriterias);
            foreach (var permission in scenarioPlanRecoveryCriteriasArg)
            {
                permission.CreatedBy = _simaIdentity.UserId;
            }
            await entity.AddScenarioPlanRecoveryCriteria(scenarioPlanRecoveryCriteriasArg);
        }

        if (request.ScenarioPossibleActions is not null && request.ScenarioPossibleActions.Count() > 0)
        {
            var scenarioPossibleActionArg = _mapper.Map<List<CreateScenarioPossibleActionArg>>(request.ScenarioPossibleActions);
            foreach (var permission in scenarioPossibleActionArg)
            {
                permission.CreatedBy = _simaIdentity.UserId;
            }
            await entity.AddScenarioPossibleAction(scenarioPossibleActionArg);
        }

        if (request.ScenarioRecoveryOptions is not null && request.ScenarioRecoveryOptions.Count() > 0)
        {
            var scenarioRecoveryOptionArg = _mapper.Map<List<CreateScenarioRecoveryOptionArg>>(request.ScenarioRecoveryOptions);
            foreach (var permission in scenarioRecoveryOptionArg)
            {
                permission.CreatedBy = _simaIdentity.UserId;
            }
            await entity.AddScenarioRecoveryOption(scenarioRecoveryOptionArg);
        }

        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }
    public async Task<Result<long>> Handle(ModifySenarioCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ScenarioId(request.Id));
        var arg = _mapper.Map<ModifyScenarioArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);

        //if (request.ScenarioBusinessContinuityPlanVersionings is not null || request.ScenarioBusinessContinuityPlanVersionings.Count() > 0)
        //{
        //    var scenarioBusinessContinuityPlanVersioningArg = _mapper.Map<List<CreateScenarioBusinessContinuityPlanVersioningArg>>(request.ScenarioBusinessContinuityPlanVersionings);
        //    foreach (var permission in scenarioBusinessContinuityPlanVersioningArg)
        //    {
        //        permission.CreatedBy = _simaIdentity.UserId;
        //    }
        //    await entity.AddScenarioBusinessContinuityPlanVersioning(scenarioBusinessContinuityPlanVersioningArg, entity.Id.Value);
        //}

        //if (request.ScenarioBusinessContinuityPlanAssumptions is not null || request.ScenarioBusinessContinuityPlanAssumptions.Count() > 0)
        //{
        //    var scenarioBusinessContinuityPlanAssumptionsArg = _mapper.Map<List<CreateScenarioBusinessContinuityPlanAssumptionArg>>(request.ScenarioBusinessContinuityPlanAssumptions);
        //    foreach (var permission in scenarioBusinessContinuityPlanAssumptionsArg)
        //    {
        //        permission.CreatedBy = _simaIdentity.UserId;
        //    }
        //    await entity.AddScenarioBusinessContinuityPlanAssumption(scenarioBusinessContinuityPlanAssumptionsArg, entity.Id.Value);
        //}

        if (request.ScenarioPlanRecoveryCriterias is not null || request.ScenarioPlanRecoveryCriterias.Count() > 0)
        {
            var scenarioPlanRecoveryCriteriasArg = _mapper.Map<List<CreateScenarioRecoveryCriteriaArg>>(request.ScenarioPlanRecoveryCriterias);
            foreach (var permission in scenarioPlanRecoveryCriteriasArg)
            {
                permission.CreatedBy = _simaIdentity.UserId;
            }
            await entity.AddScenarioPlanRecoveryCriteria(scenarioPlanRecoveryCriteriasArg);
        }

        if (request.ScenarioPossibleActions is not null || request.ScenarioPossibleActions.Count() > 0)
        {
            var scenarioPossibleActionArg = _mapper.Map<List<CreateScenarioPossibleActionArg>>(request.ScenarioPossibleActions);
            foreach (var permission in scenarioPossibleActionArg)
            {
                permission.CreatedBy = _simaIdentity.UserId;
            }
            await entity.AddScenarioPossibleAction(scenarioPossibleActionArg);
        }

        if (request.ScenarioRecoveryOptions is not null || request.ScenarioRecoveryOptions.Count() > 0)
        {
            var scenarioRecoveryOptionArg = _mapper.Map<List<CreateScenarioRecoveryOptionArg>>(request.ScenarioRecoveryOptions);
            foreach (var permission in scenarioRecoveryOptionArg)
            {
                permission.CreatedBy = _simaIdentity.UserId;
            }
            await entity.AddScenarioRecoveryOption(scenarioRecoveryOptionArg);
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteScenarioCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ScenarioId(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
