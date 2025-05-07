using AutoMapper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.InherentOccurrenceProbabilityValues;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.Args;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.Entities;

namespace SIMA.Application.Feaatures.RiskManagers.InherentOccurrenceProbabilityValues;

public class InherentOccurrenceProbabilityValueCommandHandler : ICommandHandler<CreateInherentOccurrenceProbabilityValueCommand, Result<long>>, ICommandHandler<ModifyInherentOccurrenceProbabilityValueCommand, Result<long>>
, ICommandHandler<DeleteInherentOccurrenceProbabilityValueCommand, Result<long>>
{
    private readonly IInherentOccurrenceProbabilityValueRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IInherentOccurrenceProbabilityValueDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public InherentOccurrenceProbabilityValueCommandHandler(IInherentOccurrenceProbabilityValueRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IInherentOccurrenceProbabilityValueDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateInherentOccurrenceProbabilityValueCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateInherentOccurrenceProbabilityValueArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await InherentOccurrenceProbabilityValue.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyInherentOccurrenceProbabilityValueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyInherentOccurrenceProbabilityValueArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteInherentOccurrenceProbabilityValueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}