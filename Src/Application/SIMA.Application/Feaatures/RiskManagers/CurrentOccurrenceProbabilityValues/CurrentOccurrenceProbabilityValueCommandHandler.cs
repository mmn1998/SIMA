using AutoMapper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.CurrentOccurrenceProbabilityValues;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Args;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Entities;

namespace SIMA.Application.Feaatures.RiskManagers.CurrentOccurrenceProbabilityValues;

public class CurrentOccurrenceProbabilityValueCommandHandler : ICommandHandler<CreateCurrentOccurrenceProbabilityValueCommand, Result<long>>, ICommandHandler<ModifyCurrentOccurrenceProbabilityValueCommand, Result<long>>
, ICommandHandler<DeleteCurrentOccurrenceProbabilityValueCommand, Result<long>>
{
    private readonly ICurrentOccurrenceProbabilityValueRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentOccurrenceProbabilityValueDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public CurrentOccurrenceProbabilityValueCommandHandler(ICurrentOccurrenceProbabilityValueRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, ICurrentOccurrenceProbabilityValueDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateCurrentOccurrenceProbabilityValueCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateCurrentOccurrenceProbabilityValueArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await CurrentOccurrenceProbabilityValue.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyCurrentOccurrenceProbabilityValueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyCurrentOccurrenceProbabilityValueArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteCurrentOccurrenceProbabilityValueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}