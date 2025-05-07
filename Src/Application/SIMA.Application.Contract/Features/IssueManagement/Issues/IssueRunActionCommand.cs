using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.IssueManagement.Issues;

public class IssueRunActionCommand : ICommand<Result<long>>
{
    public long IssueId { get; set; }
    public long ProgressId { get; set; }
    public long NextStepId { get; set; }
    public string? Comment { get; set; }
    public long? StepApprovalOptionId { get; set; }
    public string? ApprovalDescription { get; set; }
    public List<InputParamModel>? InputParams { get; set; }
    public List<InputDocument>? InputDocuments { get; set; }
    public List<InputParamService>? InputParamServices { get; set; }
}


public class InputDocument
{
    public long DocumentId { get; set; }
}
