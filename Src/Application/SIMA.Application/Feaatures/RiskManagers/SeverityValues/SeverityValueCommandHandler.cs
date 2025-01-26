using AutoMapper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.SeverityValues;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Args;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Entities;

namespace SIMA.Application.Feaatures.RiskManagers.SeverityValues;

public class SeverityValueCommandHandler : ICommandHandler<CreateSeverityValueCommand, Result<long>>, ICommandHandler<ModifySeverityValueCommand, Result<long>>
, ICommandHandler<DeleteSeverityValueCommand, Result<long>>
{
    private readonly ISeverityValueRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISeverityValueDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public SeverityValueCommandHandler(ISeverityValueRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, ISeverityValueDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateSeverityValueCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateSeverityValueArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await SeverityValue.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifySeverityValueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifySeverityValueArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteSeverityValueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}