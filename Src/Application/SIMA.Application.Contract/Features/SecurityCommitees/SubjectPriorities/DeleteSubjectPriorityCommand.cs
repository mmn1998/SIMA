using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.SecurityCommitees.SubjectPriorities;

public class DeleteSubjectPriorityCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
