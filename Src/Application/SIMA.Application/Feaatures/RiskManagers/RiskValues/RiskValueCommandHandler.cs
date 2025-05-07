using AutoMapper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.RiskValues;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.Entities;

namespace SIMA.Application.Feaatures.RiskManagers.RiskValues;

public class RiskValueCommandHandler : ICommandHandler<CreateRiskValueCommand, Result<long>>, ICommandHandler<ModifyRiskValueCommand, Result<long>>
, ICommandHandler<DeleteRiskValueCommand, Result<long>>
{
    private readonly IRiskValueRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRiskValueDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public RiskValueCommandHandler(IRiskValueRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IRiskValueDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateRiskValueCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateRiskValueArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await RiskValue.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        var res = entity.Id.Value;
        return Result.Ok(res);
    }

    public async Task<Result<long>> Handle(ModifyRiskValueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyRiskValueArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteRiskValueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}