using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.TrustyDrafts.InquiryRequests;

public class ModifyInquiryRequestCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? BeneficiaryName { get; set; }
    public string? ApplicantName { get; set; }
    public decimal Amount { get; set; }
    public long CurrencyTypeId { get; set; }
    public long SuggestedBrokerId { get; set; }
    public long DraftTypeId { get; set; }
    public List<CreateInquiryRequestDocumentCommand>? InquiryRequestDocuments { get; set; }
}
