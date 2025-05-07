using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.ReferalLetters;

public class GetReferalLetterQueryResult
{
    public string? LetterNumber { get; set; }
    public DateTime? LetterDate { get; set; }
    public string? LetterDatePersian => LetterDate.ToPersianDateTime();
    public long LetterDocumentId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
    public GetBroker? Borker { get; set; }
    public List<GetTrustyDraft>? TrustyDraftList { get; set; }
}
