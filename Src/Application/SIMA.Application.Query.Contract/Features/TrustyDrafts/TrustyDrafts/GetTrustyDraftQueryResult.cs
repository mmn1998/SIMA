using SIMA.Application.Query.Contract.Features.BranchManagement.BrokerTypes;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryRequests;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts.RelationDataOnTrustyDraft;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts;

public class GetTrustyDraftQueryResult
{
    public long TrustyDraftId { get; set; }
    public long DraftIssueId { get; set; }
    public string? DraftNumber { get; set; }
    public string? DraftNumberBasedOnOrder { get; set; }
    public string? DraftIssueDate { get; set; }
    public string? BlockingNumber { get; set; }
    public decimal? DraftRequestAmount { get; set; }
    public decimal? DraftRequestAmountBasedOnUsd { get; set; }
    public decimal? DraftNetAmount { get; set; }
    public string? BrokerBankName { get; set; }
    public decimal? DraftRequestNetAmountBasedOnUsd { get; set; }
    public decimal? DraftNetAmountBasedOnUsd { get; set; }
    public decimal? DraftNetAmountBasedOnEur { get; set; }
    public decimal? DraftRequestAmountBasedOnEur { get; set; }
    public TimeSpan? DraftAcceptTime { get; set; }
    public DateTime? DraftAcceptDate { get; set; }
    public string? DraftAcceptDatePersian => DraftAcceptDate.ToPersianDate();
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
    public string? PayingBankName { get; set; }
    public decimal? OriginAmount { get; set; }
    public string? DetailBic { get; set; }
    public string? OrderingExternalAccountNumber { get; set; }
    public string? IntermediateBankName { get; set; }
    public string? BeneficiaryAccountNumber { get; set; }
    public string? BeneficiaryExternalAccountNumber { get; set; }
    public string? BeneficiaryAddress { get; set; }
    public string? IssueReason { get; set; }
    public string? BeneficiaryPhoneNumber { get; set; }
    public string? BeneficiaryIban { get; set; }
    public string? BeneficiaryName { get; set; }
    public long? ReferralLetterId { get; set; }
    public string? ReferralLetterNumber { get; set; }
    public DateTime? ReferralLetterDate { get; set; }
    public string? ReferralLetterDatePersian => ReferralLetterDate.ToPersianDateTime();
    public long? ReferralLeterDocumentId { get; set; }
    public long? ReceiptDocumentId { get; set; }
    public DateTime? ReferralLetterCreatedAt { get; set; }
    public string? ReferralLetterCreatedAtPersian => ReferralLetterCreatedAt.ToPersianDateTime();
    public string? ReferralLetterCreatedBy { get; set; }
    public decimal? DraftRequestAmountOnEur { get; set; }
    public decimal? DraftRequestAmountOnUsd { get; set; }
    public decimal? BuyShareFromWage { get; set; }
    public decimal? MainShareFromWage { get; set; }
    public GetInquiryRequestQueryResult? InquiryInfo { get; set; }
    public GetBranchQueryResult? BranchInfo { get; set; }
    public GetBrokerQueryResult? BrokerInfo { get; set; }
    public GetCustomerQueryResult? CustomerInfo { get; set; }
    public GetDraftOriginQueryResult? DraftOriginInfo { get; set; }
    public GetDraftStatusQueryResult? DraftStatusInfo { get; set; }
    public GetDraftValorStatusQueryResult? DraftValorStatusInfo { get; set; }
    public GetCurrencyTypeQueryResult? CurrencyTypeInfo { get; set; }
    public IEnumerable<GetBrokerAddressResult>? BrokerAddressList { get; set; }
    public IEnumerable<GetBrokerPhoneResult>? BrokerPhoneList { get; set; }
    public IEnumerable<GetBrokerAccountResult>? BrokerAccountList { get; set; }
    public IEnumerable<GetTrustyDraftDocumentResult>? TrustyDraftDocumentList { get; set; }
    public IEnumerable<GetPaymentReceiptInfoResult>? PaymentReceiptInfoList { get; set; }
    public IEnumerable<GetStatementResult>? StatementList { get; set; }
    public IEnumerable<GetReconciliationResult>? ReconciliationList { get; set; }
    public GetBrokerSecondLevelAddressQueryResult? BorkerSecondLevelAddressInfo { get; set; }
    public GetWageDeductionMethodQueryResult? WageDeductionMethodInfo { get; set; }
    public GetAgentBankWageShareStatusResult? AgentBankWageShareStatusInfo { get; set; }
    public GetAccountTypeResult? AccountTypeInfo { get; set; }
    public GetBrokerTypeResult? BrokerTypeInfo { get; set; }
    public GetDraftTypeResult? DraftTypeInfo { get; set; }
    public GetPaymentTypeResult? PaymentTypeInfo { get; set; }
    public GetResponsibilityWageTypeResult? ResponsibilityWageTypeInfo { get; set; }
    public DateTime? ValorDate { get; set; }
    public string? ValorDatePersian => ValorDate.ToPersianDateTime();
    public string? BranchLetterNumber { get; set; }

    private TimeOnly? DraftAcceptTimeOnly
    {
        get
        {
            return DraftAcceptTime.HasValue
                ? TimeOnly.FromTimeSpan(DraftAcceptTime.Value)
                : (TimeOnly?)null;
        }
    }
}








