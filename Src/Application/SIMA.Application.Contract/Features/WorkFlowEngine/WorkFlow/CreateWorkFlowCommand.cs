using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow
{
    public class CreateWorkFlowCommand : ICommand<Result<long>>
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long ProjectId { get; set; }
        public long? ManagerRoleId { get; set; }
        public string? Description { get; set; }
    }
}
