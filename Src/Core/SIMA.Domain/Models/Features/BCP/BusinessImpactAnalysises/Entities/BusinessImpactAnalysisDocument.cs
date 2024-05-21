using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;

public class BusinessImpactAnalysisDocument : Entity
{
    private BusinessImpactAnalysisDocument()
    {

    }
    private BusinessImpactAnalysisDocument(CreateBusinessImpactAnalysisDocumentArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        DocumentId = new(arg.DocumentId);
        BusinessImpactAnalysisId = new(arg.BusinessImpactAnalysisId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessImpactAnalysisDocument Create(CreateBusinessImpactAnalysisDocumentArg arg)
    {
        return new BusinessImpactAnalysisDocument(arg);
    }
    public void Modify(ModifyBusinessImpactAnalysisDocumentArg arg)
    {
        DocumentId = new(arg.DocumentId);
        BusinessImpactAnalysisId = new(arg.BusinessImpactAnalysisId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    public BusinessImpactAnalysisDocumentId Id { get; private set; }
    public DocumentId DocumentId { get; private set; }
    public virtual Document Document { get; private set; }
    public BusinessImpactAnalysisId BusinessImpactAnalysisId { get; private set; }
    public virtual BusinessImpactAnalysis BusinessImpactAnalysis { get; private set; }
    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}