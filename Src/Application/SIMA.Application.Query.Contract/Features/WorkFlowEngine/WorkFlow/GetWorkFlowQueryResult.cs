namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow
{
    public class GetWorkFlowQueryResult
    {

        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? ActiveStatus { get; set; }
        public long? ManagerRoleId { get; set; }
        public long? DomainId { get; set; }
        public string? DomainName { get; set; }
        public float? Ordering { get; set; }
        public string? BpmnId { get; set; }
        public string FileContent { get; set; }
        public string? Description { get; set; }
        public int? ActiveStatusId { get; set; }

    }
}
