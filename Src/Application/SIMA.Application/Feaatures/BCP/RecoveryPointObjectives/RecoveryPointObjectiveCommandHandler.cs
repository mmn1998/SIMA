using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.RecoveryPointObjectives;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Args;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Contracts;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Entities;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.RecoveryPointObjectives;

public class RecoveryPointObjectiveCommandHandler : ICommandHandler<CreateRecoveryPointObjectiveCommand, Result<long>>,
    ICommandHandler<ModifyRecoveryPointObjectiveCommand, Result<long>>, ICommandHandler<DeleteRecoveryPointObjectiveCommand, Result<long>>
{
    private readonly IRecoveryPointObjectiveRepository _repository;
    private readonly IRecoveryPointObjectiveDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public RecoveryPointObjectiveCommandHandler(IRecoveryPointObjectiveRepository repository, IRecoveryPointObjectiveDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateRecoveryPointObjectiveCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateRecoveryPointObjectiveArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await RecoveryPointObjective.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyRecoveryPointObjectiveCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new RecoveryPointObjectiveId(request.Id));
        var arg = _mapper.Map<ModifyRecoveryPointObjectiveArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteRecoveryPointObjectiveCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new RecoveryPointObjectiveId(request.Id));
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}