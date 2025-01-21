using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.PlanResponsibilities;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Args;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Contracts;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.PlanResponsibilities;

public class PlanResponsibilityCommandHandler : ICommandHandler<CreatePlanResponsibilityCommand, Result<long>>,
    ICommandHandler<ModifyPlanResponsibilityCommand, Result<long>>, ICommandHandler<DeletePlanResponsibilityCommand, Result<long>>
{
    private readonly IPlanResponsibilityRepository _repository;
    private readonly IPlanResponsibilityDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public PlanResponsibilityCommandHandler(IPlanResponsibilityRepository repository, IPlanResponsibilityDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreatePlanResponsibilityCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreatePlanResponsibilityArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await PlanResponsibility.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyPlanResponsibilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyPlanResponsibilityArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeletePlanResponsibilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}