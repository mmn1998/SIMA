using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.IssueManagement.IssueLinkReasons;
using SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Args;
using SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.IssueManagement.IssueLinkReasons;

public class IssueLinkReasonCommandHandler : ICommandHandler<CreateIssueLinkReasonCommand, Result<long>>, ICommandHandler<ModifyIssueLinkReasonCommand, Result<long>>
, ICommandHandler<DeleteIssueLinkReasonCommand, Result<long>>
{
    private readonly IIssueLinkReasonRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIssueLinkReasonDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public IssueLinkReasonCommandHandler(IIssueLinkReasonRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IIssueLinkReasonDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateIssueLinkReasonCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateIssueLinkReasonArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await IssueLinkReason.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyIssueLinkReasonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyIssueLinkReasonArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteIssueLinkReasonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
