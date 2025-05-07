namespace SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityPlans
{
    public class GetBusinesscontinuityplanversioning
    {
        public long Id { get; set; }
        public string? VersionNumber { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
