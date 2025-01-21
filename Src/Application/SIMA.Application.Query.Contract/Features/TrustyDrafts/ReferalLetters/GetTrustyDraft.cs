using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.ReferalLetters;

public class GetTrustyDraft
{
    public long TrustyDraftId { get; set; }
    public string? DraftNumber { get; set; }
    public string? DraftNumberBasedOnOrder { get; set; }
    public GetBranch? Branch { get; set; }
    public GetCustomer? Customer { get; set; }
    public string? DraftIssueDate { get; set; }
    public decimal? DraftReguestAmount { get; set; }
    public decimal? DraftReguestAmountBasedOnUsd { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
    public GetDraftCurrencyType? DraftCurrencyType { get; set; }
}

public class GetBranch
{
    public long TrustyDraftId { get; set; }
    public long BranchId { get; set; }
    public string? BranchName { get; set; }
    public string? BranchCode { get; set; }
}

public class GetCustomer
{
    public long TrustyDraftId { get; set; }
    public long CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerNumber { get; set; }
}
public class GetDraftCurrencyType
{
    public long TrustyDraftId { get; set; }
    public long CurrencyTypeId { get; set; }
    public string? CurrencyTypeName { get; set; }
    public string? CurrencyTypeCode { get; set; }
}
