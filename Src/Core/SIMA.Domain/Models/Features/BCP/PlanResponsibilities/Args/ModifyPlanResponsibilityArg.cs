namespace SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Args
{
    public class ModifyPlanResponsibilityArg
    {
        public long Id { get; set; }
        public string? Name { get;  set; }
        public string? Code { get;  set; }
        public long ActiveStatusId { get;  set; }
        public float Ordering { get;  set; }
        public byte[]? ModifiedAt { get;  set; }
        public long? ModifiedBy { get;  set; }
    }
}
