using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.IssueManagement.Issues
{
    public class CreateIssueCommand : ICommand<Result<long>>
    {
        public long CurrentWorkflowId { get; set; }
        public long MainAggregateId { get; set; }
        public long SourceId { get; set; }
        public long? IssueTypeId { get; set; }
        public long? IssuePriorityId { get; set; }
        public int? Weight { get; set; }
        public string Summery { get; set; }
        public string? Description { get; set; }
        public string? DueDate { get; set; }

        public List<CreateIssueLinkCommand>? IssueLinks { get; set; }
        public List<CreateIssueDocumentCommand>? IssueDocument { get; set; }
    }
}
