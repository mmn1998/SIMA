using AutoMapper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Contracts;
using SIMA.Application.Contract.Features.RiskManagers.RiskLevelCobits;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Entities;

namespace SIMA.Application.Feaatures.RiskManagers.RiskLevelCobits;

public class RiskLevelCobitCommandHandler : ICommandHandler<CreateRiskLevelCobitCommand, Result<long>>, ICommandHandler<ModifyRiskLevelCobitCommand, Result<long>>
, ICommandHandler<DeleteRiskLevelCobitCommand, Result<long>>
{
    private readonly IRiskLevelCobitRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRiskLevelCobitDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public RiskLevelCobitCommandHandler(IRiskLevelCobitRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IRiskLevelCobitDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateRiskLevelCobitCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateRiskLevelCobitArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await RiskLevelCobit.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyRiskLevelCobitCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyRiskLevelCobitArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteRiskLevelCobitCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}