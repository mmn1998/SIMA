using SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts.RelationDataOnTrustyDraft;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts
{
    public class GetTrustyDraftInqueryQueryResult
    {
        public long TrustyDraftId { get; set; }
        public string? DraftNumber { get; set; }
        public string? DraftIssueDate { get; set; }
        public string? BlockingNumber { get; set; }
        public decimal? DraftRequestAmount { get; set; }
        public decimal? DraftNetAmount { get; set; }
        public string? DraftNumberBasedOnOrder { get; set; }
        public decimal? DraftRequestAmountOnUsd { get; set; }
        public decimal? DraftRequestAmountOnEur { get; set; }
        public decimal? DraftNetAmountBasedOnUsd { get; set; }
        public decimal? DraftNetAmountBasedOnEur { get; set; }
        public decimal? DraftRequestAmountBasedOnEur { get; set; }
        public decimal? DraftRequestAmountBasedOnUsd { get; set; }
        public decimal? DraftRequestNetAmountBasedOnUsd { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
        public string? CreatedBy { get; set; }
        public string? PayingBankName { get; set; }
        public decimal? OriginAmount { get; set; }
        public string? DetailBic { get; set; }
        public string? OrderingExternalAccountNumber { get; set; }
        public string? IntermediateBankName { get; set; }
        public string? BeneficiaryIban { get; set; }
        public string? BeneficiaryName { get; set; }
        public string? BeneficiaryAddress { get; set; }
        public string? BeneficiaryPhoneNumber { get; set; }
        public string? BeneficiaryAccountNumber { get; set; }
        public string? BeneficiaryExternalAccountNumber { get; set; }
        public long? ReceiptDocumentId { get; set; }
        public DateTime? ValorDate { get; set; }
        public string? ValorDatePersian => ValorDate.ToPersianDate();
        public string? BrokerBankName { get; set; }
        public string? AgentBank { get; set; }
        public string? IssueReason { get; set; }
        public TimeSpan? DraftAcceptTime { get; set; }
        public DateTime? DraftAcceptDate { get; set; }
        public string? DraftAcceptDatePersian => DraftAcceptDate.ToPersianDate();
        public DateTime? ReferralLetterCreatedAt { get; set; }
        public string? ReferralLetterCreatedAtPersian => ReferralLetterCreatedAt.ToPersianDateTime();
        public string? ReferralLetterCreatedBy { get; set; }
        public GetBranchQueryResult? BranchInfo { get; set; }
        public GetCustomerQueryResult? CustomerInfo { get; set; }
        public GetDraftOriginQueryResult? DraftOriginInfo { get; set; }
        public GetDraftStatusQueryResult? DraftStatusInfo { get; set; }
        public GetDraftValorStatusQueryResult? DraftValorStatusInfo { get; set; }
        public GetCurrencyTypeQueryResult? CurrencyTypeInfo { get; set; }
        public GetDraftTypeResult? DraftTypeInfo { get; set; }
        public GetResponsibilityWageTypeResult? ResponsibilityWageTypeInfo { get; set; }
        public GetAccountTypeResult? AccountTypeInfo { get; set; }
        public GetDraftReviewResult? DraftReviewResultInfo { get; set; }
        public GetCancellationResaonResult? CancellationResaonInfo { get; set; }
        public GetLoanTypeResult? LoanTypeInfo { get; set; }
        public GetDraftDestinationResult? DraftDestinationInfo { get; set; }
        public GetBrokerTypeResult? BrokerTypeInfo { get; set; }

        private TimeOnly? DraftAcceptTimeOnly
        {
            get
            {
                return DraftAcceptTime.HasValue
                    ? TimeOnly.FromTimeSpan(DraftAcceptTime.Value)
                    : (TimeOnly?)null;
            }
        }
        public string? DraftAcceptTimeNormalized => DateHelper.ToTimeOnly(DraftAcceptTimeOnly);

    }
}
