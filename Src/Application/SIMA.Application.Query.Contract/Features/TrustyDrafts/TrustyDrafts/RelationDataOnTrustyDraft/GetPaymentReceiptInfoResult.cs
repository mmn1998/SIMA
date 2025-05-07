using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts.RelationDataOnTrustyDraft
{
    public class GetPaymentReceiptInfoResult
    {
        public long Id { get; set; }
        public string? ReceiptNumber { get; set; }
        public long? CurrencyTypeId { get; set; }
        public string? CurrencyTypeName { get; set; }
        public string? CurrencyTypeCode { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentDatePersian => PaymentDate.ToPersianDateTime();
        public decimal? PaymentAmount { get; set; }
        public long? PaymentTypeId { get; set; }
        public string? PaymentTypeName { get; set; }
        public long? BrokerAccountBookId { get; set; }
        public string? BrokerIbanNumber { get; set; }
        public long? BeneficiaryAccountTypeId { get; set; }
        public string? BeneficiaryAccountTypeName { get; set; }
        public string? BeneficiaryAccountNumber { get; set; }
        public long? TrustyDraftDocumentId { get; set; }
        public long? DocumentId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
        public string? CreatedBy { get; set; }
    }
}
