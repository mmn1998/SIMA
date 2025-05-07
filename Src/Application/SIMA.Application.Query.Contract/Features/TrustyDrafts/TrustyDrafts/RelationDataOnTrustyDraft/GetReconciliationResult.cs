using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts.RelationDataOnTrustyDraft
{
    public class GetReconciliationResult
    {
        public long Id { get; set; }
        public long? ReconciliationTypeId { get; set; }
        public string? ReconciliationTypeName { get; set; }
        public string? IsInformedByBranch { get; set; }
        public DateTime? InformedDate { get; set; }
        public string? InformedDatePersian => InformedDate.ToPersianDateTime();
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
        public string? CreatedBy { get; set; }
    }
}
