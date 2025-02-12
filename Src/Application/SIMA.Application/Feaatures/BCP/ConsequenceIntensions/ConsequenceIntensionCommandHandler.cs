using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.ConsequenceIntensions;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Args;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Contracts;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.ConsequenceIntensions;

public class ConsequenceIntensionCommandHandler : ICommandHandler<CreateConsequenceIntensionCommand, Result<long>>,
    ICommandHandler<ModifyConsequenceIntensionCommand, Result<long>>, ICommandHandler<DeleteConsequenceIntensionCommand, Result<long>>
{
    private readonly IConsequenceIntensionRepository _repository;
    private readonly IConsequenceIntensionDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ConsequenceIntensionCommandHandler(IConsequenceIntensionRepository repository, IConsequenceIntensionDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateConsequenceIntensionCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateConsequenceIntensionArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ConsequenceIntension.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyConsequenceIntensionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyConsequenceIntensionArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteConsequenceIntensionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}