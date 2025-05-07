namespace SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityPlans
{
    public class GetBusinesscontinuityplanrisk
    {
        public long id { get; set; }
        public string? description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
