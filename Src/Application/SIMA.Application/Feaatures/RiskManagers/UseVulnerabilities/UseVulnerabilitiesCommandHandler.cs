using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.TriggerStatuses;
using SIMA.Application.Contract.Features.RiskManagers.UseVulnerabilities;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.UseVulnerabilities.Args;
using SIMA.Domain.Models.Features.RiskManagement.UseVulnerabilities.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.UseVulnerabilities.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;

namespace SIMA.Application.Feaatures.RiskManagers.UseVulnerabilities;

public class UseVulnerabilitiesCommandHandler :  ICommandHandler<CreateUseVulnerabilityCommand, Result<long>>, ICommandHandler<ModifyUseVulnerabilityCommand, Result<long>>
    , ICommandHandler<DeleteUseVulnerabilityCommand, Result<long>>
{
    private readonly IUseVulnerabilityRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUseVulnerabilityDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public UseVulnerabilitiesCommandHandler(IUseVulnerabilityRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IUseVulnerabilityDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }

    public async Task<Result<long>> Handle(CreateUseVulnerabilityCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateUseVulnerabilityArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await UseVulnerability.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyUseVulnerabilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyUseVulnerabilityArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteUseVulnerabilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}