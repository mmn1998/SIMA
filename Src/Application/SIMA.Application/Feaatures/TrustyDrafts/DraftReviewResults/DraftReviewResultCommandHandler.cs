using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftReviewResults;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftReviewResults;

public class DraftReviewResultCommandHandler : ICommandHandler<CreateDraftReviewResultCommand, Result<long>>,
    ICommandHandler<ModifyDraftReviewResultCommand, Result<long>>, ICommandHandler<DeleteDraftReviewResultCommand, Result<long>>
{
    private readonly IDraftReviewResultRepository _repository;
    private readonly IDraftReviewResultDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public DraftReviewResultCommandHandler(IDraftReviewResultRepository repository, IDraftReviewResultDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateDraftReviewResultCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateDraftReviewResultArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await DraftReviewResult.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyDraftReviewResultCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyDraftReviewResultArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteDraftReviewResultCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}