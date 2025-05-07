using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.Consequences;
using SIMA.Domain.Models.Features.BCP.Consequences.Args;
using SIMA.Domain.Models.Features.BCP.Consequences.Contracts;
using SIMA.Domain.Models.Features.BCP.Consequences.Entities;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.Consequences;

public class ConsequenceCommandHandler : ICommandHandler<CreateConsequenceCommand, Result<long>>,
    ICommandHandler<ModifyConsequenceCommand, Result<long>>, ICommandHandler<DeleteConsequenceCommand, Result<long>>
{
    private readonly IConsequenceRepository _repository;
    private readonly IConsequenceDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ConsequenceCommandHandler(IConsequenceRepository repository, IConsequenceDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateConsequenceCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateConsequenceArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Consequence.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyConsequenceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ConsequenceId(request.Id));
        var arg = _mapper.Map<ModifyConsequenceArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteConsequenceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ConsequenceId(request.Id));
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}