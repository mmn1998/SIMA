using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.SecurityCommitees.SubjectPriorities;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.SecurityCommitees.SubjectPriorities;

public class SubjectPriorityCommandHandler : ICommandHandler<CreateSubjectPriorityCommand, Result<long>>, ICommandHandler<ModifySubjectPriorityCommand, Result<long>>,
    ICommandHandler<DeleteSubjectPriorityCommand, Result<long>>
{
    private readonly ISubjectPriorityRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubjectPriorityDomainService _service;
    private readonly IMapper _mapper;
    private readonly ISimaIdentity _simaIdentity;

    public SubjectPriorityCommandHandler(ISubjectPriorityRepository repository, IUnitOfWork unitOfWork,
        ISubjectPriorityDomainService service, IMapper mapper, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _service = service;
        _mapper = mapper;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateSubjectPriorityCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateSubjectPriorityArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await SubjectPriority.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return entity.Id.Value;
    }

    public async Task<Result<long>> Handle(ModifySubjectPriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifySubjectPriorityArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return entity.Id.Value;
    }

    public async Task<Result<long>> Handle(DeleteSubjectPriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return entity.Id.Value;
    }
}
