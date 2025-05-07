using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.ConsequenceIntensionDescriptions;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensionDescriptions.Args;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensionDescriptions.Contracts;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensionDescriptions.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.ConsequenceIntensionDescriptions;

public class ConsequenceIntensionDescriptionCommandHandler : ICommandHandler<CreateConsequenceIntensionDescriptionCommand, Result<long>>,
    ICommandHandler<ModifyConsequenceIntensionDescriptionCommand, Result<long>>, ICommandHandler<DeleteConsequenceIntensionDescriptionCommand, Result<long>>
{
    private readonly IConsequenceIntensionDescriptionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ConsequenceIntensionDescriptionCommandHandler(IConsequenceIntensionDescriptionRepository repository,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateConsequenceIntensionDescriptionCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateConsequenceIntensionDescriptionArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = ConsequenceIntensionDescription.Create(arg);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyConsequenceIntensionDescriptionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyConsequenceIntensionDescriptionArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        entity.Modify(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteConsequenceIntensionDescriptionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}