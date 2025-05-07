namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.ReferalLetters
{
    public class GetBroker
    {
        public long BrokerId { get; set; }
        public string? BrokerName { get; set; }
        public string? BrokerCode { get; set; }
    }
}
