namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor
{
    public class GetWorkFlowActorQueryResult
    {
        public long Id { get; set; }

        public string? Name { get; set; }
        public long? WorkFlowId { get; set; }
        public string? WorkFlowName { get; set; }
        public long? DomainId { get; set; }
        public string? DomainName { get; set; }
        public long? ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? ActiveStatus { get; set; }

        public string? Code { get; set; }

        public long? ActiveStatusId { get; set; }
        public List<WorkFlowActorGroup>? WorkFlowActorGroups { get; set; }
        public List<WorkFlowActorRole>? WorkFlowActorRoles { get; set; }
        public List<WorkFlowActorUser>? WorkFlowActorUsers { get; set; }
    }

    public class WorkFlowActorRole
    {
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public string  RoleCode { get; set; }
    }

    public class WorkFlowActorGroup
    {
        public long GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupCode { get; set; }
    }

    public class WorkFlowActorUser
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}
