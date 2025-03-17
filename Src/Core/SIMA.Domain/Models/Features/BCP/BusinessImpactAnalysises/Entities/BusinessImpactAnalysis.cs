using SIMA.Domain.Models.Features.Auths.TimeMeasurements.Entities;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Events;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.ValueObjects;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Entities;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.ValueObjects;
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
        Id = new(arg.Id);
        ActiveStatusId = arg.ActiveStatusId;
        RecoveryPointObjectiveId = new(arg.RecoveryPointObjectiveId);
        TimeMeasurementId = new(arg.TimeMeasurementId);
        ServiceId = new(arg.ServiceId);
        RecoveryPointObjectiveId = new(arg.RecoveryPointObjectiveId);
        RTO = arg.RTO;
        WRT = arg.WRT;
        MTPD = arg.MTPD;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ///todo:get Mahyar new Api
        //AddDomainEvent(new CreateBusinessImpactAnalysisEvent
        //    (issueId: arg.IssueId, mainAggregateType: MainAggregateEnums.BusinessImpactAnalysis,
        //    name: arg.RestartReason, sourceId: arg.Id, issuePriorityId: arg.IssuePriorityId, issueWeightCategoryId: arg.IssueWeightCategoryId));
    }
    public static async Task<BusinessImpactAnalysis> Create(CreateBusinessImpactAnalysisArg arg, IBusinessImpactAnalysisDomainService service)
    {
        await CreateGuards(arg, service);
        return new BusinessImpactAnalysis(arg);
    }
    public async Task Modify(ModifyBusinessImpactAnalysisArg arg, IBusinessImpactAnalysisDomainService service)
    {
        await ModifyGuards(arg, service);
        ServiceId = new(arg.ServiceId);
        RecoveryPointObjectiveId = new(arg.RecoveryPointObjectiveId);
        TimeMeasurementId = new(arg.TimeMeasurementId);
        RTO = arg.RTO;
        WRT = arg.WRT;
        MTPD = arg.MTPD;
        ActiveStatusId = arg.ActiveStatusId;
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
    #region AddMethods
    public void AddBusinessImpactAnalysisAssets(List<CreateBusinessImpactAnalysisAssetArg> args)
    {
        foreach (var arg in args)
        {
            var entity = BusinessImpactAnalysisAsset.Create(arg);
            _businessImpactAnalysisAssets.Add(entity);
        }
    }
    public void AddBusinessImpactAnalysisStaffs(List<CreateBusinessImpactAnalysisStaffArg> args)
    {
        foreach (var arg in args)
        {
            var entity = Entities.BusinessImpactAnalysisStaff.Create(arg);
            _businessImpactAnalysisStaff.Add(entity);
        }
    }
    public void AddBusinessImpactAnalysisDocuments(List<CreateBusinessImpactAnalysisDocumentArg> args)
    {
        foreach (var arg in args)
        {
            var entity = BusinessImpactAnalysisDocument.Create(arg);
            _businessImpactAnalysisDocuments.Add(entity);
        }
    }
    public void AddBusinessImpactAnalysisDisasterOrigins(List<CreateBusinessImpactAnalysisDisasterOriginArg> args)
    {
        foreach (var arg in args)
        {
            var entity = BusinessImpactAnalysisDisasterOrigin.Create(arg);
            _businessImpactAnalysisDisasterOrigins.Add(entity);
        }
    }
    public void AddBusinessImpactAnalysisIssues(CreateBusinessImpactAnalysisIssueArg arg)
    {
        var entity = BusinessImpactAnalysisIssue.Create(arg);
        _businessImpactAnalysisIssues.Add(entity);

    }
    #endregion
    #region ModifyMethods
    public void ModifyBusinessImpactAnalysisAssets(List<CreateBusinessImpactAnalysisAssetArg> args)
    {
        var activeEntities = _businessImpactAnalysisAssets.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.AssetId == x.AssetId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.AssetId.Value == x.AssetId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _businessImpactAnalysisAssets.FirstOrDefault(x => x.AssetId.Value == arg.AssetId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = BusinessImpactAnalysisAsset.Create(arg);
                _businessImpactAnalysisAssets.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyBusinessImpactAnalysisDisasterOrigins(List<CreateBusinessImpactAnalysisDisasterOriginArg> args)
    {
        var activeEntities = _businessImpactAnalysisDisasterOrigins.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.OriginId == x.OriginId.Value && c.ConsequenceId == x.ConsequenceId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.OriginId.Value == x.OriginId && c.ConsequenceId.Value == x.ConsequenceId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _businessImpactAnalysisDisasterOrigins.FirstOrDefault(x => x.OriginId.Value == arg.OriginId && x.ConsequenceId.Value == arg.ConsequenceId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = BusinessImpactAnalysisDisasterOrigin.Create(arg);
                _businessImpactAnalysisDisasterOrigins.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyBusinessImpactAnalysisStaffs(List<CreateBusinessImpactAnalysisStaffArg> args)
    {
        var activeEntities = _businessImpactAnalysisStaff.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.StaffId == x.StaffId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.StaffId.Value == x.StaffId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _businessImpactAnalysisStaff.FirstOrDefault(x => x.StaffId.Value == arg.StaffId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = Entities.BusinessImpactAnalysisStaff.Create(arg);
                _businessImpactAnalysisStaff.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyBusinessImpactAnalysisDocuments(List<CreateBusinessImpactAnalysisDocumentArg> args)
    {
        var activeEntities = _businessImpactAnalysisDocuments.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.DocumentId == x.DocumentId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.DocumentId.Value == x.DocumentId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _businessImpactAnalysisDocuments.FirstOrDefault(x => x.DocumentId.Value == arg.DocumentId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = BusinessImpactAnalysisDocument.Create(arg);
                _businessImpactAnalysisDocuments.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    #endregion
    #region DeleteMethods
    public void DeleteBusinessImpactAnalysisAssets(long userId)
    {
        foreach (var entity in _businessImpactAnalysisAssets)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteBusinessImpactAnalysisStaffs(long userId)
    {
        foreach (var entity in _businessImpactAnalysisStaff)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteBusinessImpactAnalysisDocuments(long userId)
    {
        foreach (var entity in _businessImpactAnalysisDocuments)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteBusinessImpactAnalysisIssues(long userId)
    {
        foreach (var entity in _businessImpactAnalysisIssues)
        {
            entity.Delete(userId);
            AddDomainEvent(new DeleteBusinessImpactAnalysisEvent
    (issueId: entity.IssueId.Value));
        }
    }
    public void DeleteBusinessImpactAnalysisDisasterOrigins(long userId)
    {
        foreach (var entity in _businessImpactAnalysisDisasterOrigins)
        {
            entity.Delete(userId);
        }
    }
    #endregion
    #region Properties

    public BusinessImpactAnalysisId Id { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual Service Service { get; private set; }
    public RecoveryPointObjectiveId RecoveryPointObjectiveId { get; private set; }
    public virtual RecoveryPointObjective RecoveryPointObjective { get; private set; }
    public TimeMeasurementId TimeMeasurementId { get; private set; }
    public virtual TimeMeasurement TimeMeasurement { get; private set; }


    public float RTO { get; set; }
    public float WRT { get; set; }
    public float? MTPD { get; set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        #region DeleteRalatedEntities
        DeleteBusinessImpactAnalysisAssets(userId);
        DeleteBusinessImpactAnalysisStaffs(userId);
        DeleteBusinessImpactAnalysisDocuments(userId);
        DeleteBusinessImpactAnalysisDisasterOrigins(userId);
        DeleteBusinessImpactAnalysisIssues(userId);
        #endregion


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
    private List<BusinessImpactAnalysisIssue> _businessImpactAnalysisIssues = new();
    public ICollection<BusinessImpactAnalysisIssue> BusinessImpactAnalysisIssues => _businessImpactAnalysisIssues;
}