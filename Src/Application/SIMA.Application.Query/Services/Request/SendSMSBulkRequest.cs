namespace SIMA.Application.Query.Services.Request
{
    public class SendSMSBulkRequest
    {
        public string SourceAddress { get; set; }
        public List<string> DestinationAddress { get; set; }
        public string MessageText { get; set; }
        public DateTime ValidityPeriod { get; set; }
    }
}
