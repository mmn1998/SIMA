using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowActor;

public class CreateRelatedWorkFlowActorEntitiesCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public List<long>? RoleId { get; set; }
    public List<long>? UserId { get; set; }
    public List<long>? GroupId { get; set; }
}
