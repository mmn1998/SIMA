using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.SecurityCommitees.Meetings;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.SecurityCommitees.Meetings;

public class MeetingCommandHandler : ICommandHandler<CreateMeetingCommands, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMeetingRepository _meetingRepository;
    private readonly IMeetingDomainService _meetingDomainService;
    private readonly IWorkFlowRepository _workFlowRepository;
    private readonly ISimaIdentity _simaIdentity;

    public MeetingCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IMeetingRepository meetingRepository,
        IMeetingDomainService meetingDomainService, IWorkFlowRepository workFlowRepository,
        ISimaIdentity simaIdentity)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _meetingRepository = meetingRepository;
        _meetingDomainService = meetingDomainService;
        _workFlowRepository = workFlowRepository;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateMeetingCommands request, CancellationToken cancellationToken)
    {

        try
        {
            var arg = _mapper.Map<CreateMeetingArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var meeting = await Meeting.Create(arg, _meetingDomainService);

            //MeetingDocument

            if (request.MeetingsDocument is not null && request.MeetingsDocument.Count < 0)
            {
                var documentArg = _mapper.Map<List<CreateMeetingDocumentArg>>(request.MeetingsDocument);
                foreach (var document in documentArg) document.CreatedBy = _simaIdentity.UserId;
                meeting.CreateMeetingDocument(documentArg);
            }

            //MeetingReason

            var reasonArg = _mapper.Map<List<CreateMeetingReasonArg>>(request.Reasons);
            foreach (var reason in reasonArg) reason.CreatedBy = _simaIdentity.UserId;
            meeting.CreateMeetingReason(reasonArg);

            //MeetingSubject

            foreach (var subject in arg.NewSubject)
            {
                request.ExistsSubjects.Add(new ExistsSubjectcommand { SubjectId = subject.Id });
            }

            var subjectMeetingArg = _mapper.Map<List<CreateSubjectMeetingArg>>(request.ExistsSubjects);
            foreach (var subject in subjectMeetingArg) subject.CreatedBy = _simaIdentity.UserId;
            subjectMeetingArg.ForEach(it => it.MeetingId = meeting.Id.Value);
            meeting.CreateMeetingSubject(subjectMeetingArg);

            await _meetingRepository.Add(meeting);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok(meeting.Id.Value);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
