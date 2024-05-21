using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Entities;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Domain.Models.Features.BCP.Consequences.Entities;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Entities;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.ValueObjects;
using SIMA.Domain.Models.Features.BCP.MaximumAcceptableOutages.Entities;
using SIMA.Domain.Models.Features.BCP.MaximumAcceptableOutages.ValueObjects;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.Entities;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

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
        if (arg.MaximumAcceptableOutageId.HasValue) MaximumAcceptableOutageId = new(arg.MaximumAcceptableOutageId.Value);
        if (arg.ConsequenceId.HasValue) ConsequenceId = new(arg.ConsequenceId.Value);
        RTO = arg.RTO;
        MTD = arg.MTD;
        WRT = arg.WRT;
        RPO = arg.RPO;
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
        if (arg.MaximumAcceptableOutageId.HasValue) MaximumAcceptableOutageId = new(arg.MaximumAcceptableOutageId.Value);
        if (arg.ConsequenceId.HasValue) ConsequenceId = new(arg.ConsequenceId.Value);
        RTO = arg.RTO;
        MTD = arg.MTD;
        WRT = arg.WRT;
        RPO = arg.RPO;
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
    public ServicePriorityId ServicePriorityId { get; private set; }
    public virtual ServicePriority ServicePriority { get; private set; }
    public BackupPeriodId BackupPeriodId { get; private set; }
    public virtual BackupPeriod BackupPeriod { get; private set; }
    public MaximumAcceptableOutageId? MaximumAcceptableOutageId { get; private set; }
    public virtual MaximumAcceptableOutage? MaximumAcceptableOutage { get; private set; }
    public ConsequenceId? ConsequenceId { get; private set; }
    public virtual Consequence? Consequence { get; private set; }
    
    /// TODO : ServiceId
    //public long ServiceId { get; private set; }
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
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<BusinessImpactAnalysisDocument> _businessImpactAnalysisDocuments = new();
    public ICollection<BusinessImpactAnalysisDocument> BusinessImpactAnalysisDocuments => _businessImpactAnalysisDocuments;
    private List<BusinessImpactAnalysisAnnouncement> _businessImpactAnalysisAnnouncements = new();
    public ICollection<BusinessImpactAnalysisAnnouncement> BusinessImpactAnalysisAnnouncements => _businessImpactAnalysisAnnouncements;
    private List<BusinessImpactAnalysisAsset> _businessImpactAnalysisAssets = new();
    public ICollection<BusinessImpactAnalysisAsset> BusinessImpactAnalysisAssets => _businessImpactAnalysisAssets;
}
