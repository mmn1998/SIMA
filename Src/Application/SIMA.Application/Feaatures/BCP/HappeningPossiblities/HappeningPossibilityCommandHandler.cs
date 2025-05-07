using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.HappeningPossiblities;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Args;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Contracts;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Entities;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.HappeningPossiblities;

public class HappeningPossibilityCommandHandler : ICommandHandler<CreateHappeningPossibilityCommand, Result<long>>,
    ICommandHandler<ModifyHappeningPossibilityCommand, Result<long>>, ICommandHandler<DeleteHappeningPossibilityCommand, Result<long>>
{
    private readonly IHappeningPossibilityRepository _repository;
    private readonly IHappeningPossibilityDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public HappeningPossibilityCommandHandler(IHappeningPossibilityRepository repository, IHappeningPossibilityDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateHappeningPossibilityCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateHappeningPossibilityArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await HappeningPossibility.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyHappeningPossibilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new HappeningPossibilityId(request.Id));
        var arg = _mapper.Map<ModifyHappeningPossibilityArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteHappeningPossibilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new HappeningPossibilityId(request.Id));
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
