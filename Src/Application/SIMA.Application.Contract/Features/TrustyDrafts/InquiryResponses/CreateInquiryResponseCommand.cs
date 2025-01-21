using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.TrustyDrafts.InquiryResponses;

public class CreateInquiryResponseCommand : ICommand<Result<long>>
{
    public long BrokerInquiryStatusId { get; set; }
    public long InquiryRequestId { get; set; }
    public long InquiryRequestCurrencyId { get; set; }
    public long BrokerId { get; set; }
    public long WageRateId { get; set; }
    public decimal CalculatedWage { get; set; }
    public decimal ExcessWage { get; set; }
    public string? ValidityPeriod { get; set; }
    public string? ResponseDescription { get; set; }
}
