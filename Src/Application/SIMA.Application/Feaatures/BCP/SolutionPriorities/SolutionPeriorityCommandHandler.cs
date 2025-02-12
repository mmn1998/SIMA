using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.SolutionPriorities;
using SIMA.Domain.Models.Features.BCP.SolutionPeriorities.Args;
using SIMA.Domain.Models.Features.BCP.SolutionPriorities.Contracts;
using SIMA.Domain.Models.Features.BCP.SolutionPriorities.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.SolutionPriorities;

public class SolutionPriorityCommandHandler : ICommandHandler<CreateSolutionPriorityCommand, Result<long>>,
    ICommandHandler<ModifySolutionPriorityCommand, Result<long>>, ICommandHandler<DeleteSolutionPriorityCommand, Result<long>>
{
    private readonly ISolutionPriorityRepository _repository;
    private readonly ISolutionPriorityDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public SolutionPriorityCommandHandler(ISolutionPriorityRepository repository, ISolutionPriorityDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateSolutionPriorityCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateSolutionPriorityArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await SolutionPriority.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifySolutionPriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifySolutionPriorityArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteSolutionPriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}