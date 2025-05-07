using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.Project
{
    public class CreateProjectGroupCommand : ICommand<Result<long>>
    {
        public long ProjectId { get; set; }
        public List<long> GroupId { get; set; }
    }
}
