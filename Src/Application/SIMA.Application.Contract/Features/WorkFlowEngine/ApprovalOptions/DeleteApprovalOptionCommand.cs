using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.ApprovalOptions
{
    public class DeleteApprovalOptionCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
