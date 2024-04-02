using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.IssueManagement.Issues;

public class ModifyIssueCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long IssueTypeId { get; set; }
    public string Description { get; set; }
    public string Summery { get; set; }
    public long IssuePriorityId { get; set; }
    public long IssueWeightCategoryd { get; set; }
    public int Weight { get; set; }
    public DateTime DueDate { get; set; }

}
