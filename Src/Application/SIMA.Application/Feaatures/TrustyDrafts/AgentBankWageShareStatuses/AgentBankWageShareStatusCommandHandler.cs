using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.AgentBankWageShareStatuses;
using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.AgentBankWageShareStatuses;

public class AgentBankWageShareStatusCommandHandler : ICommandHandler<CreateAgentBankWageShareStatusCommand, Result<long>>,
ICommandHandler<ModifyAgentBankWageShareStatusCommand, Result<long>>, ICommandHandler<DeleteAgentBankWageShareStatusCommand, Result<long>>
{
    private readonly IAgentBankWageShareStatusRepository _repository;
    private readonly IAgentBankWageShareStatusDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public AgentBankWageShareStatusCommandHandler(IAgentBankWageShareStatusRepository repository, IAgentBankWageShareStatusDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateAgentBankWageShareStatusCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateAgentBankWageShareStatusArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await AgentBankWageShareStatus.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyAgentBankWageShareStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyAgentBankWageShareStatusArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteAgentBankWageShareStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
