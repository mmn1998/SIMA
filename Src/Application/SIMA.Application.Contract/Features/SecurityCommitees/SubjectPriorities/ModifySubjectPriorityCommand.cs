using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.SecurityCommitees.SubjectPriorities;

public class ModifySubjectPriorityCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public float? Ordering { get; set; }
}