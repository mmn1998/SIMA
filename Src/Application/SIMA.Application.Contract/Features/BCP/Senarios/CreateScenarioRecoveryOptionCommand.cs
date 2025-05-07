namespace SIMA.Application.Contract.Features.BCP.Senarios
{
    public class CreateScenarioRecoveryOptionCommand
    {
        public string Description { get; set; }
        public long RecoveryOptionPriorityId { get; set; }
    }
}
