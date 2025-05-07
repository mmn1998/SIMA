using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.TriggerStatuses;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.Args;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;

namespace SIMA.Application.Feaatures.RiskManagers.TriggerStatuses;

public class TriggerStatusCommandHandler: ICommandHandler<CreateTriggerStatusCommand, Result<long>>, ICommandHandler<ModifyTriggerStatusCommand, Result<long>>
    , ICommandHandler<DeleteTriggerStatusCommand, Result<long>>
{
    private readonly ITriggerStatusRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITriggerStatusDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public TriggerStatusCommandHandler(ITriggerStatusRepository repository, IUnitOfWork unitOfWork, IMapper mapper, ITriggerStatusDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateTriggerStatusCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateTriggerStatusArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await TriggerStatus.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyTriggerStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyTriggerStatusArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteTriggerStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}