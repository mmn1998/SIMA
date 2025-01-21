namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.CurrencyPaymentChannels;

public class GetCurrencyPaymentChannelQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
    public long LocationId { get; set; }
    public string? LocationName { get; set; }
}