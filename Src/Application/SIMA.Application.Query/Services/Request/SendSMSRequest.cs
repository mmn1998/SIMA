namespace SIMA.Application.Query.Services.Request
{
    public class SendSMSRequest
    {
        public string SourceAddress { get; set; }
        public string DestinationAddress { get; set; }
        public string MessageText { get; set; }
        public DateTime ValidityPeriod { get; set; }
    }
}
