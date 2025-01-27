using AutoMapper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.Severities;
using SIMA.Domain.Models.Features.RiskManagement.Severities.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.Severities.Args;
using SIMA.Domain.Models.Features.RiskManagement.Severities.Entities;

namespace SIMA.Application.Feaatures.RiskManagers.AffectedHistories;

public class SeverityCommandHandler : ICommandHandler<CreateSeverityCommand, Result<long>>, ICommandHandler<ModifySeverityCommand, Result<long>>
, ICommandHandler<DeleteSeverityCommand, Result<long>>
{
    private readonly ISeverityRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISeverityDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public SeverityCommandHandler(ISeverityRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, ISeverityDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateSeverityCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateSeverityArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Severity.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifySeverityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifySeverityArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteSeverityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}