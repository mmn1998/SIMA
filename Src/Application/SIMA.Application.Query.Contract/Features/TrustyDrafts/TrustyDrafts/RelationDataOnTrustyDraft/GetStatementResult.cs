using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts.RelationDataOnTrustyDraft
{
    public class GetStatementResult
    {
        public long Id { get; set; }
        public DateTime? StatementDate { get; set; }
        public string? StatementDatePersian => StatementDate.ToPersianDateTime();
        public string? Description { get; set; }
        public long? TrustyDraftDocumentId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
        public string? CreatedBy { get; set; }
    }
}
