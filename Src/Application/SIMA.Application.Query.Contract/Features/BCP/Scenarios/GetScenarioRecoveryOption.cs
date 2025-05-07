namespace SIMA.Application.Query.Contract.Features.BCP.Scenarios
{
    public class GetScenarioRecoveryOption
    {
        public long Id { get; set; }
        public string? Description { get; set; }
        public long RecoveryOptionPriorityId { get; set; }
        public string? RecoveryOptionPriorityName { get; set; }
        public DateTime CreateAt { get; set; }

    }
}
