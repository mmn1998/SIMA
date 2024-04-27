using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Events
{
    public sealed record MeetingCreatedEvent(long issueId,MainAggregateEnums MainAggregateType, string Name, long SourceId , IEnumerable<CreateLabelArg> labels , IEnumerable<CreateSubjectArg> SubjectArgs) : IDomainEvent;
       
}
