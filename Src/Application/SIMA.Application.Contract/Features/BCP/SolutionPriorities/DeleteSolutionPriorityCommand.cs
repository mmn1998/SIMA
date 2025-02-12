using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.SolutionPriorities;

public class DeleteSolutionPriorityCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}