using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow
{
    public class DeleteWorkFlowCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
