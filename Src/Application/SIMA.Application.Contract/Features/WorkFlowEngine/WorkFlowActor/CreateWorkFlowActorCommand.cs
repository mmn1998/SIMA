using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowActor
{
    public class CreateWorkFlowActorCommand : ICommand<Result<long>>
    {
       
        public List<long>? RoleId { get; set; }
        public List<long>? UserId { get; set; }
        public List<long>? GroupId { get; set; }
        public string? Name { get; set; }
        public long WorkFlowId { get; set; }
        public string? Code { get; set; }

    }
}
