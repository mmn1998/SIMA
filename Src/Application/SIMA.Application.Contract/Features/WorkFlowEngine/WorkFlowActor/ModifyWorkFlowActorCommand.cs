using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowActor;

public class ModifyWorkFlowActorCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsDirectManagerOfIssueCreator { get; set; }
    public string? IsEveryOne { get; set; }
    public List<long>? UserId { get; set; }
    public List<long>? RoleId { get; set; }
    public List<long>? GroupId { get; set; }
    public string? IsActorManager { get; set; }
    public List<long>? EmployeeId { get; set; }
}
