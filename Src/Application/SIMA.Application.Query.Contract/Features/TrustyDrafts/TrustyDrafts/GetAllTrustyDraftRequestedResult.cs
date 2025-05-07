using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts;

public class GetAllTrustyDraftRequestedResult
{
    public long TrustyDraftId { get; set; }
    public string? DraftNumber { get; set; }
    public string? DraftNumberBasedOnOrder { get; set; }
    public long? BranchId { get; set; }
    public string? BranchName { get; set; }
    public string? BranchCode { get; set; }
    public long? BrokerId { get; set; }
    public string? BrokerName { get; set; }
    public string? BrokerCode { get; set; }
    public string? DraftIssueCode { get; set; }
    public DateTime? DraftIssueDate { get; set; }
    public string? DraftIssueDatePersian => DraftIssueDate.ToPersianDateTime();
    public long? DraftIssueId { get; set; }
    public string? DraftIssueCurrentStepName { get; set; }
    public long? DraftIssueCurrentStepId { get; set; }
    public decimal? DraftRequestAmount { get; set; }
    public decimal? DraftRequestAmountBasedOnUsd { get; set; }
    public decimal? DraftNetAmount { get; set; }
    public decimal? DraftRequestNetAmountBasedOnUsd { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
    public long? CurrencyTypeId { get; set; }
    public string? CurrencyTypeName { get; set; }
    public string? CurrencyTypeCode { get; set; }
    public string? PayingBankName { get; set; }
    public string? IntermediateBankName { get; set; }
    public long? DraftValorStatusId { get; set; }
    public string? DraftValorStatusName { get; set; }
    public string? DraftValorStatusCode { get; set; }
    public long? DraftStatusId { get; set; }
    public string? DraftStatusName { get; set; }
    public string? DraftStatusCode { get; set; }
    public long? WageDetuctionMethodId { get; set; }
    public string? WageDetuctionMethodName { get; set; }
    public string? WageDetuctionMethodCode { get; set; }
    public string? DraftTypeName { get; set; }
    public long? DraftTypeId { get; set; }
    public string? BrokerTypeName { get; set; }
    public long? BrokerTypeId { get; set; }
    public string? PaymentTypeName { get; set; }
    public long? PaymentTypeId { get; set; }
    public string? CustomerName { get; set; }
    public long? CustomerId { get; set; }
    public DateTime? ValorDate { get; set; }
    public string? ValorDatePersian => ValorDate.ToPersianDateTime();
    public string? BranchLetterNumber { get; set; }
    public decimal? BuyShareFromWage { get; set; }
    public decimal? MainShareFromWage { get; set; }
}
