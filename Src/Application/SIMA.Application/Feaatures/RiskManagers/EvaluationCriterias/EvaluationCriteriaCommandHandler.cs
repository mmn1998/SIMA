using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.EvaluationCriterias;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Args;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.RiskManagers.EvaluationCriterias;

public class EvaluationCriteriaCommandHandler : ICommandHandler<CreateEvaluationCriteriaCommand, Result<long>>,
    ICommandHandler<ModifyEvaluationCriteriaCommand, Result<long>>, ICommandHandler<DeleteEvaluationCriteriaCommand, Result<long>>
{
    private readonly IEvaluationCriteriaRepository _repository;
    private readonly IEvaluationCriteriaDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public EvaluationCriteriaCommandHandler(IEvaluationCriteriaRepository repository, IEvaluationCriteriaDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateEvaluationCriteriaCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateEvaluationCriteriaArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await EvaluationCriteria.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyEvaluationCriteriaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyEvaluationCriteriaArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteEvaluationCriteriaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}