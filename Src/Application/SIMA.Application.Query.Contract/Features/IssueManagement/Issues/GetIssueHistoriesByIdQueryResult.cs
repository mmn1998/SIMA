using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues
{
    public class GetIssueHistoriesByIdQueryResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? IssueId { get; set; }
        public long SourceStateId { get; set; }
        public long TargetStateId { get; set; }
        public long SourceStepId { get; set; }
        public long TargetStepId { get; set; }
        public long PerformerUserId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedAtPersian => DateHelper.ToPersianDateTime(CreatedAt);
        public string CreatedBy { get; set; }
    }
}
