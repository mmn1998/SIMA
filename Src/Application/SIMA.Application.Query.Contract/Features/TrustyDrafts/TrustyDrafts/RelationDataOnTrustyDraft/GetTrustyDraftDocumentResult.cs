using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.TrustyDrafts.TrustyDrafts.RelationDataOnTrustyDraft
{
    public class GetTrustyDraftDocumentResult
    {
        public long Id { get; set; }
        public long DocumentId { get; set; }
        public long DocumentTypeId { get; set; }
        public string? DocumentTypeName { get; set; }
        public string? DocumentFileName { get; set; }
        public long FileExtensionId { get; set; }
        public string? DocumentContentType { get; set; }
        public string? DocumentExtensionName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
        public string? CreatedBy { get; set; }
        public string? AttachStepName { get; set; }
    }
}
