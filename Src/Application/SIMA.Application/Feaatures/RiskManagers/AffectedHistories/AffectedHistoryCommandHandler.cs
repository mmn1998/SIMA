using AutoMapper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.AffectedHistories;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.Args;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.Entities;

namespace SIMA.Application.Feaatures.RiskManagers.AffectedHistories;

public class AffectedHistoryCommandHandler : ICommandHandler<CreateAffectedHistoryCommand, Result<long>>, ICommandHandler<ModifyAffectedHistoryCommand, Result<long>>
, ICommandHandler<DeleteAffectedHistoryCommand, Result<long>>
{
    private readonly IAffectedHistoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAffectedHistoryDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public AffectedHistoryCommandHandler(IAffectedHistoryRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IAffectedHistoryDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateAffectedHistoryCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateAffectedHistoryArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await AffectedHistory.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyAffectedHistoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyAffectedHistoryArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteAffectedHistoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId;
        await entity.Delete(userId, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}