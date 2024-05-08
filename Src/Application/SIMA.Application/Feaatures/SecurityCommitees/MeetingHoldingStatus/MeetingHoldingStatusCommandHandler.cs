using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.SecurityCommitees.MeetingHoldingStatus;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.SecurityCommitees.MeetingHoldingStatus;

public class MeetingHoldingStatusCommandHandler : ICommandHandler<CreateMeetingHoldingStatusCommand, Result<long>>, ICommandHandler<ModifyMeetingHoldingStatusCommand, Result<long>>,
ICommandHandler<DeleteMeetingHoldingStatusCommand, Result<long>>
{
    private readonly IMeetingHoldingStatusRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMeetingHoldingStatusDomainService _service;
    private readonly IMapper _mapper;
    private readonly ISimaIdentity _simaIdentity;

    public MeetingHoldingStatusCommandHandler(IMeetingHoldingStatusRepository repository, IUnitOfWork unitOfWork,
        IMeetingHoldingStatusDomainService service, IMapper mapper, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _service = service;
        _mapper = mapper;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateMeetingHoldingStatusCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateMeetingHoldingStatusArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Entities.MeetingHoldingStatus.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return entity.Id.Value;
    }

    public async Task<Result<long>> Handle(ModifyMeetingHoldingStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyMeetingHoldingStatusArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return entity.Id.Value;
    }

    public async Task<Result<long>> Handle(DeleteMeetingHoldingStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return entity.Id.Value;
    }
}
