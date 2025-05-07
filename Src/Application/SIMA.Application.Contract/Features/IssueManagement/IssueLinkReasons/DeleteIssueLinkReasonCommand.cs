using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.IssueManagement.IssueLinkReasons
{
    public class DeleteIssueLinkReasonCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
