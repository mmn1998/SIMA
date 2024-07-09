using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Entities;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Domain.Models.Features.BCP.Consequences.Entities;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Entities;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.ValueObjects;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.Entities;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;

public class BusinessImpactAnalysis : Entity, IAggregateRoot
{
    private BusinessImpactAnalysis()
    {

    }
    private BusinessImpactAnalysis(CreateBusinessImpactAnalysisArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ImportanceDegreeId = new(arg.ImportanceDegreeId);
        ServicePriorityId = new(arg.ServicePriorityId);
        BackupPeriodId = new(arg.BackupPeriodId);
        ServiceId = new(arg.ServiceId);
        RestartReason = arg.RestartReason;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<BusinessImpactAnalysis> Create(CreateBusinessImpactAnalysisArg arg, IBusinessImpactAnalysisDomainService service)
    {
        await CreateGuards(arg, service);
        return new BusinessImpactAnalysis(arg);
    }
    public async Task Modify(ModifyBusinessImpactAnalysisArg arg, IBusinessImpactAnalysisDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ImportanceDegreeId = new(arg.ImportanceDegreeId);
        ServicePriorityId = new(arg.ServicePriorityId);
        BackupPeriodId = new(arg.BackupPeriodId);
        RestartReason = arg.RestartReason;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateBusinessImpactAnalysisArg arg, IBusinessImpactAnalysisDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyBusinessImpactAnalysisArg arg, IBusinessImpactAnalysisDomainService service)
    {

    }
    #endregion
    public BusinessImpactAnalysisId Id { get; private set; }
    public ImportanceDegreeId ImportanceDegreeId { get; private set; }
    public virtual ImportanceDegree ImportanceDegree { get; private set; }
    public OrganizationalServicePriorityId ServicePriorityId { get; private set; }
    public virtual OrganizationalServicePriority ServicePriority { get; private set; }
    public BackupPeriodId BackupPeriodId { get; private set; }
    public virtual BackupPeriod BackupPeriod { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual Service Service { get; private set; }
    public string? Name { get; private set; }
    public float? RTO { get; private set; }
    public float? RPO { get; private set; }
    public float? WRT { get; private set; }
    public float? MTD { get; private set; }
    public string? RestartReason { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<BusinessImpactAnalysisDocument> _businessImpactAnalysisDocuments = new();
    public ICollection<BusinessImpactAnalysisDocument> BusinessImpactAnalysisDocuments => _businessImpactAnalysisDocuments;
    private List<BusinessImpactAnalysisAnnouncement> _businessImpactAnalysisAnnouncements = new();
    public ICollection<BusinessImpactAnalysisAnnouncement> BusinessImpactAnalysisAnnouncements => _businessImpactAnalysisAnnouncements;
    private List<BusinessImpactAnalysisAsset> _businessImpactAnalysisAssets = new();
    public ICollection<BusinessImpactAnalysisAsset> BusinessImpactAnalysisAssets => _businessImpactAnalysisAssets;
    private List<BusinessImpactAnalysisCriticalActivity> _businessImpactAnalysisCriticalActivities = new();
    public ICollection<BusinessImpactAnalysisCriticalActivity> BusinessImpactAnalysisCriticalActivities => _businessImpactAnalysisCriticalActivities;
    private List<BusinessImpactAnalysisStaff> _businessImpactAnalysisStaff = new();
    public ICollection<BusinessImpactAnalysisStaff> BusinessImpactAnalysisStaff => _businessImpactAnalysisStaff;
    private List<BusinessImpactAnalysisDisasterOrigin> _businessImpactAnalysisDisasterOrigins = new();
    public ICollection<BusinessImpactAnalysisDisasterOrigin> BusinessImpactAnalysisDisasterOrigins => _businessImpactAnalysisDisasterOrigins;
}