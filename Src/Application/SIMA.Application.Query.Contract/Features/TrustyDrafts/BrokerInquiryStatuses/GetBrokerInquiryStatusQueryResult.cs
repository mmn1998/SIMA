namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.BrokerInquiryStatuses;

public class GetBrokerInquiryStatusQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}