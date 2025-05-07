using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftValorStatuses;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftValorStatuses;

public class DraftValorStatusCommandHandler : ICommandHandler<CreateDraftValorStatusCommand, Result<long>>,
    ICommandHandler<ModifyDraftValorStatusCommand, Result<long>>, ICommandHandler<DeleteDraftValorStatusCommand, Result<long>>
{
    private readonly IDraftValorStatusRepository _repository;
    private readonly IDraftValorStatusDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public DraftValorStatusCommandHandler(IDraftValorStatusRepository repository, IDraftValorStatusDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateDraftValorStatusCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateDraftValorStatusArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await DraftValorStatus.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyDraftValorStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyDraftValorStatusArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteDraftValorStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}