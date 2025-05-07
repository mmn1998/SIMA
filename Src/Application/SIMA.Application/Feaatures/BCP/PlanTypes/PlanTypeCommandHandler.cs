using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.PlanTypes;
using SIMA.Domain.Models.Features.BCP.PlanTypes.Args;
using SIMA.Domain.Models.Features.BCP.PlanTypes.Contracts;
using SIMA.Domain.Models.Features.BCP.PlanTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.PlanTypes;

public class PlanTypeCommandHandler : ICommandHandler<CreatePlanTypeCommand, Result<long>>,
    ICommandHandler<ModifyPlanTypeCommand, Result<long>>, ICommandHandler<DeletePlanTypeCommand, Result<long>>
{
    private readonly IPlanTypeRepository _repository;
    private readonly IPlanTypeDomianService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public PlanTypeCommandHandler(IPlanTypeRepository repository, IPlanTypeDomianService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreatePlanTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreatePlanTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await PlanType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyPlanTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyPlanTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeletePlanTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}