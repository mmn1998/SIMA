using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.ConsequenceValues;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.Args;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.Contracts;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.ConsequenceValues;

public class ConsequenceValueCommandHandler : ICommandHandler<CreateConsequenceValueCommand, Result<long>>,
    ICommandHandler<ModifyConsequenceValueCommand, Result<long>>, ICommandHandler<DeleteConsequenceValueCommand, Result<long>>
{
    private readonly IConsequenceValueRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ConsequenceValueCommandHandler(IConsequenceValueRepository repository,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateConsequenceValueCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateConsequenceValueArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = ConsequenceValue.Create(arg);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyConsequenceValueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyConsequenceValueArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        entity.Modify(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteConsequenceValueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}