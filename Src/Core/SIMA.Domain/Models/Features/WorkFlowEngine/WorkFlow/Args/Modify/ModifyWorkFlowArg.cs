namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify
{
    public class ModifyWorkFlowArg
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long ProjectId { get; set; }
        public string? BpmnId { get; private set; }
        public long MainAggregateId { get; set; }
        public float? Ordering { get; set; }
        public long? ManagerRoleId { get; set; }
        public string? Description { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }

    }
}
