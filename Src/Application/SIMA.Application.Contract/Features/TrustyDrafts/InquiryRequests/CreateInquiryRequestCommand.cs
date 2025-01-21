using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.TrustyDrafts.InquiryRequests;

public class CreateInquiryRequestCommand : ICommand<Result<long>>
{
    public string? BeneficiaryName { get; set; }
    public string? DraftOrderNumber { get; set; }
    public string? ProformaNumber { get; set; }   
    public string? RequestDescription { get; set; }   
    public long CustomerId { get; set; }
    public long PaymentTypeId { get; set; }
    public long DraftOriginId { get; set; }
    public string? DraftOrderDate { get; set; }
    public string? ProformaDate { get; set; }
    public decimal? ProformaAmount { get; set; }
    public long? ProformaCurrencyTypeId { get; set; }
    public List<CreateInquiryRequestDocumentCommand>? InquiryRequestDocuments { get; set; }
    public List<CreateInquiryRequestCurrencyCommand>? InquiryRequestCurrencies { get; set; }
}
public class CreateInquiryRequestDocumentCommand
{
    public long DocumentId { get; set; }
}
public class CreateInquiryRequestCurrencyCommand
{
    public decimal Amount { get; set; }
    public long CurrencyTypeId { get; set; }

}
