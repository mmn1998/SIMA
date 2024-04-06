using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.IssueManagement.Issues
{
    public class IssueRunActionCommand : ICommand<Result<long>>
    {
        public long IssueId { get; set; }
        public long ProgressId { get; set; }
        public long NextStepId { get; set; }
        public string? Comment { get; set; }
    }
}
