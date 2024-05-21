using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;

public class BusinessImpactAnalysisAnnouncement : Entity
{
    private BusinessImpactAnalysisAnnouncement()
    {

    }
    private BusinessImpactAnalysisAnnouncement(CreateBusinessImpactAnalysisAnnouncementArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        StaffId = new(arg.StaffId);
        BusinessImpactAnalysisId = new(arg.BusinessImpactAnalysisId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessImpactAnalysisAnnouncement Create(CreateBusinessImpactAnalysisAnnouncementArg arg)
    {
        return new BusinessImpactAnalysisAnnouncement(arg);
    }
    public void Modify(ModifyBusinessImpactAnalysisAnnouncementArg arg)
    {
        StaffId = new(arg.StaffId);
        BusinessImpactAnalysisId = new(arg.BusinessImpactAnalysisId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    public BusinessImpactAnalysisAnnouncementId Id { get; private set; }
    public StaffId StaffId { get; private set; }
    public virtual Staff Staff { get; private set; }
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