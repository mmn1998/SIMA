using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.SecurityCommitees.MeetingHoldingReasons;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.SecurityCommitees.MeetingHoldingReasons;

public class MeetingHoldingReasonCommandHandler : ICommandHandler<CreateMeetingHoldingReasonCommand, Result<long>>,
    ICommandHandler<ModifyMeetingHoldingReasonCommand, Result<long>>,
    ICommandHandler<DeleteMeetingHoldingReasonCommand, Result<long>>
{
    private readonly IMeetingHoldingReasonRepository _repository;
    private readonly IMeetingHoldingReasonDomainService _domainService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MeetingHoldingReasonCommandHandler(IMeetingHoldingReasonRepository repository,
        IMeetingHoldingReasonDomainService domainService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _domainService = domainService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateMeetingHoldingReasonCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateMeetingHoldingReasonArg>(request);
        var entity = await MeetingHoldingReason.Create(arg, _domainService);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyMeetingHoldingReasonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyMeetingHoldingReasonArg>(request);
        await entity.Modify(arg, _domainService);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(DeleteMeetingHoldingReasonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
