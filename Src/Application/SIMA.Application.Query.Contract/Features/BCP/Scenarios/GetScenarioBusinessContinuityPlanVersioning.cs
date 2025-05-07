namespace SIMA.Application.Query.Contract.Features.BCP.Scenarios
{
    public class GetScenarioBusinessContinuityPlanVersioning
    {
        public long Id{ get; set; }
        public string? BusinessContinuityPlanTitle { get; set; }
        public string? VersionNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
