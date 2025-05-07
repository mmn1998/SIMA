using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.ReferalLetters
{
    public class GetAllReferralLettersQueryResult
    {
        public long Id { get; set; }
        public string? LetterNumber { get; set; }
        public DateTime? LetterDate { get; set; }
        public string? LetterDatePersian => LetterDate.ToPersianDateTime();
        public long LetterDocumentId { get; set; }
        public long TrustyDraftId { get; set; }
        public string? DraftNumber { get; set; }
        public string? DraftNumberBasedOnOrder { get; set; }
        public long BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? BranchCode { get; set; }
        public long CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerNumber { get; set; }
        public DateTime? DraftIssueDate { get; set; }
        public string? DraftIssueDatePersian => DraftIssueDate.ToPersianDateTime();
        public decimal DraftRequestAmount { get; set; }
        public decimal DraftRequestAmountBasedOnUsd { get; set; }
        public long CurrencyTypeId { get; set; }
        public string? CurrencyTypeName { get; set; }
        public string? CurrencyTypeCode { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
        public string? CreatedBy { get; set; }
        public long BrokerId { get; set; }
        public string? BrokerName { get; set; }
        public string? BrokerCode { get; set; }
        public long? PaymentTypeId { get; set; }
        public string? PaymentTypeName { get; set; }
        public long? DraftStatusId { get; set; }
        public string? DraftStatusName { get; set; }
        public long? IssueId { get; set; }
        public string? IssueCode { get; set; }
        public long? BrokerTypeId { get; set; }
        public string? BrokerTypeName { get; set; }
        public string? DraftIssueCurrentStepName { get; set; }

    }
}
