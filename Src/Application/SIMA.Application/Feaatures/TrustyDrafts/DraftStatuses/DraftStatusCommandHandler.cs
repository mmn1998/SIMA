using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftStatuses;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftStatuses;

public class DraftStatusCommandHandler : ICommandHandler<CreateDraftStatusCommand, Result<long>>,
    ICommandHandler<ModifyDraftStatusCommand, Result<long>>, ICommandHandler<DeleteDraftStatusCommand, Result<long>>
{
    private readonly IDraftStatusRepository _repository;
    private readonly IDraftStatusDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public DraftStatusCommandHandler(IDraftStatusRepository repository, IDraftStatusDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateDraftStatusCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateDraftStatusArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await DraftStatus.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyDraftStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyDraftStatusArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteDraftStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}