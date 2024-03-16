using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.IssueManagement.IssuePriorities;
using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Args;
using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.IssueManagement.IssuePriorities;

public class IssuePriorityCommandHandler : ICommandHandler<CreateIssuePriorityCommand, Result<long>>, ICommandHandler<ModifyIssuePriorityCommand, Result<long>>
    , ICommandHandler<DeleteIssuePriorityCommand, Result<long>>
{
    private readonly IIssuePriorityRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIssuePriorityDomainService _service;

    public IssuePriorityCommandHandler(IIssuePriorityRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IIssuePriorityDomainService service)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
    }
    public async Task<Result<long>> Handle(CreateIssuePriorityCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateIssuePriorityArg>(request);
        var entity = await IssuePriority.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyIssuePriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyIssuePriorityArg>(request);
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteIssuePriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
