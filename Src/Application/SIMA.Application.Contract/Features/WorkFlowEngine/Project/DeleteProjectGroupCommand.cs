using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.Project
{
    public class DeleteProjectGroupCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
    }
}
