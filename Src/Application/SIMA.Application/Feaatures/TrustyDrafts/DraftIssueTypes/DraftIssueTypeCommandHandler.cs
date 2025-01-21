using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftIssueTypes;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftIssueTypes;

public class DraftIssueTypeCommandHandler : ICommandHandler<CreateDraftIssueTypeCommand, Result<long>>,
    ICommandHandler<ModifyDraftIssueTypeCommand, Result<long>>, ICommandHandler<DeleteDraftIssueTypeCommand, Result<long>>
{
    private readonly IDraftIssueTypeRepository _repository;
    private readonly IDraftIssueTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public DraftIssueTypeCommandHandler(IDraftIssueTypeRepository repository, IDraftIssueTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateDraftIssueTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateDraftIssueTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await DraftIssueType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyDraftIssueTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyDraftIssueTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteDraftIssueTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}