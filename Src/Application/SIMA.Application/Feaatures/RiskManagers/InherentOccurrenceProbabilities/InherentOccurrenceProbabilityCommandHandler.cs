using AutoMapper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Contracts;
using SIMA.Application.Contract.Features.RiskManagers.InherentOccurrenceProbabilities;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Args;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Entities;

namespace SIMA.Application.Feaatures.RiskManagers.InherentOccurrenceProbabilities;

public class InherentOccurrenceProbabilityCommandHandler : ICommandHandler<CreateInherentOccurrenceProbabilityCommand, Result<long>>, ICommandHandler<ModifyInherentOccurrenceProbabilityCommand, Result<long>>
, ICommandHandler<DeleteInherentOccurrenceProbabilityCommand, Result<long>>
{
    private readonly IInherentOccurrenceProbabilityRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IInherentOccurrenceProbabilityDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public InherentOccurrenceProbabilityCommandHandler(IInherentOccurrenceProbabilityRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IInherentOccurrenceProbabilityDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateInherentOccurrenceProbabilityCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateInherentOccurrenceProbabilityArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await InherentOccurrenceProbability.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyInherentOccurrenceProbabilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyInherentOccurrenceProbabilityArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteInherentOccurrenceProbabilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}