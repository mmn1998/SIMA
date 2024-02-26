using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.IssueManagement.Issues
{
    public class DeleteIssueCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
