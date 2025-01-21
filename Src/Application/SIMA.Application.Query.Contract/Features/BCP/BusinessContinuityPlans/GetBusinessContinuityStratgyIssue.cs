namespace SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityPlans
{
    public class GetBusinessContinuityStratgyIssue
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
