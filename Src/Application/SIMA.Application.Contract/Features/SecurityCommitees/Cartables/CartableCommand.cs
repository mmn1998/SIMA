using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.SecurityCommitees.Cartables;

public class CartableCommand : ICommand<Result<long>>
{
    public long? IssueId { get; set; }
    public long? ProgressId { get; set; }
    public long? NextStepId { get; set; }
    public string? SpName { get; set; }
    public string? ConditionValue { get; set; }
}

