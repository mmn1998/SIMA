using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.Auths.Positions.Entities;
using SIMA.Domain.Models.Features.Auths.Positions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Args;
using SIMA.Domain.Models.Features.Auths.Staffs.Exceptions;
using SIMA.Domain.Models.Features.Auths.Staffs.Interfaces;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Staffs.Entities;

public class Staff : Entity
{
    private Staff() { }
    private Staff(CreateStaffArg arg)
    {
        Id = new StaffId(IdHelper.GenerateUniqueId());
        if (arg.ProfileId.HasValue) ProfileId = new ProfileId(arg.ProfileId.Value);
        if (arg.PositionId.HasValue) PositionId = new PositionId(arg.PositionId.Value);
        if (arg.ManagerId.HasValue) ManagerId = new ProfileId(arg.ManagerId.Value);
        StaffNumber = arg.StaffNumber;
        ActiveFrom = arg.ActiveFrom;
        ActiveTo = arg.ActiveTo;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public async Task Modify(ModifyStaffArg arg, IStaffService service)
    {
        await ModifyGuards(arg, service);
        if (arg.ProfileId.HasValue) ProfileId = new ProfileId(arg.ProfileId.Value);
        if (arg.PositionId.HasValue) PositionId = new PositionId(arg.PositionId.Value);
        if (arg.ManagerId.HasValue) ManagerId = new ProfileId(arg.ManagerId.Value);
        StaffNumber = arg.StaffNumber;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }

    public static async Task<Staff> Create(CreateStaffArg arg, IStaffService service)
    {
        await CreateGuards(arg, service);
        return new Staff(arg);
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    #region Guards
    private static async Task CreateGuards(CreateStaffArg arg, IStaffService service)
    {
        arg.NullCheck();
        arg.ProfileId.NullCheck();
        arg.ManagerId.NullCheck();
        arg.PositionId.NullCheck();

        if (arg.PositionId.HasValue && arg.ProfileId.HasValue)
        {
            if (!await service.IsPositionDuplicated(arg.PositionId.Value, arg.ProfileId.Value)) throw new SimaResultException(CodeMessges._100011Code, Messages.PersonHasDuplicatePositionError);
            if (!await service.IsStaffSatisfied(arg.ProfileId.Value, arg.PositionId.Value)) throw StaffExceptions.StaffNotSatisfiedException;
        }
    }
    private async Task ModifyGuards(ModifyStaffArg arg, IStaffService service)
    {
        arg.NullCheck();
        arg.ProfileId.NullCheck();
        arg.ManagerId.NullCheck();
        arg.PositionId.NullCheck();

        if (arg.PositionId.HasValue && arg.ProfileId.HasValue)
        {
            if (!await service.IsPositionDuplicated(arg.PositionId.Value, arg.ProfileId.Value)) throw new SimaResultException(CodeMessges._100011Code, Messages.PersonHasDuplicatePositionError);
            if (!await service.IsStaffSatisfied(arg.ProfileId.Value, arg.PositionId.Value)) throw StaffExceptions.StaffNotSatisfiedException;
        }
    }
    #endregion
    public StaffId Id { get; private set; }

    public ProfileId? ProfileId { get; private set; }

    public ProfileId? ManagerId { get; private set; }

    public PositionId? PositionId { get; private set; }

    public string? StaffNumber { get; private set; }

    public DateOnly? ActiveFrom { get; private set; }

    public DateOnly? ActiveTo { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual Profile? Manager { get; private set; }

    public virtual Position? Position { get; private set; }

    public virtual Profile? Profile { get; private set; }
    public virtual ICollection<Branch> ChiefBranches { get; private set; }
    public virtual ICollection<Branch> DeputyBranches { get; private set; }

    private List<Approval> _responsibleApprovals => new();
    public ICollection<Approval> ResponsibleApprovals => _responsibleApprovals;
    private List<Approval> _supervisorApprovals => new();
    public ICollection<Approval> SupervisorApprovals => _supervisorApprovals;
    private List<Invitees> _invitees => new();
    public ICollection<Invitees> Invitees => _invitees;

    private List<CriticalActivity> _criticalActivityTechnicalResponsibles => new();
    public ICollection<CriticalActivity> CriticalActivityTechnicalResponsibles => _criticalActivityTechnicalResponsibles;

    private List<CriticalActivity> _criticalActivityTechnicalSupervisors => new();
    public ICollection<CriticalActivity> CriticalActivityTechnicalSupervisors => _criticalActivityTechnicalSupervisors;

    private List<CriticalActivityAssignedStaff> _criticalActivityAssignStaffs => new();
    public ICollection<CriticalActivityAssignedStaff> CriticalActivityAssignStaffs => _criticalActivityAssignStaffs;

 

    private List<Service> _serviceTechnicalResponsibles = new();
    public ICollection<Service> ServiceTechnicalResponsibles => _serviceTechnicalResponsibles;
    private List<Service> _serviceBusinessResponsibles = new();
    public ICollection<Service> ServiceTBusinessResponsibles => _serviceBusinessResponsibles;
    private List<Service> _serviceTechnicalSupports = new();
    public ICollection<Service> ServiceTechnicalSupports => _serviceTechnicalSupports;
    private List<Service> _serviceTechnicalSupervisors = new();
    public ICollection<Service> ServiceTechnicalSupervisors => _serviceTechnicalSupervisors;
    private List<ServiceAssignedStaff> _serviceAssignStaffes = new();
    public ICollection<ServiceAssignedStaff> ServiceAssignStaffes => _serviceAssignStaffes;

   
    private List<Api> _apis = new();
    public ICollection<Api> ResponsibleApis => _apis;
    private List<ApiSupportTeam> _apiSupportTeams = new();
    public ICollection<ApiSupportTeam> ApiSupportTeams => _apiSupportTeams;
    private List<BusinessImpactAnalysisAnnouncement> _businessImpactAnalysisAnnouncements => new();
    public ICollection<BusinessImpactAnalysisAnnouncement> BusinessImpactAnalysisAnnouncements => _businessImpactAnalysisAnnouncements;

    private List<BusinessContinuityPlan> _businessContinuityPlanOwners => new();
    public ICollection<BusinessContinuityPlan> BusinessContinuityPlanOwners => _businessContinuityPlanOwners;
    private List<BusinessContinuityPlan> _businessContinuityPlanExecutiveResponsibles => new();
    public ICollection<BusinessContinuityPlan> BusinessContinuityPlanExecutiveResponsibles => _businessContinuityPlanExecutiveResponsibles;
    private List<BusinessContinuityPlan> _businessContinuityPlanRecoveryManagers => new();
    public ICollection<BusinessContinuityPlan> BusinessContinuityPlanRecoveryManagers => _businessContinuityPlanRecoveryManagers;
    private List<BusinessContinuityPlan> _businessContinuityPlanRecoveryDeputy => new();
    public ICollection<BusinessContinuityPlan> BusinessContinuityPlanRecoveryDeputy => _businessContinuityPlanRecoveryDeputy;
    private List<BusinessContinuityPlanRelatedStaff> _BusinessContinuityPlanRelatedStaff => new();
    public ICollection<BusinessContinuityPlanRelatedStaff> BusinessContinuityPlanRelatedStaff => _BusinessContinuityPlanRelatedStaff;
    private List<BusinessContinuityStrategyStaff> _businessContinuityStrategyStaff => new();
    public ICollection<BusinessContinuityStrategyStaff> BusinessContinuityStrategyStaff => _businessContinuityStrategyStaff;
    private List<BusinessImpactAnalysisStaff> _businessImpactAnalysisStaff => new();
    public ICollection<BusinessImpactAnalysisStaff> BusinessImpactAnalysisStaff => _businessImpactAnalysisStaff;
    private List<ProductResponsible> _productResponsibles => new();
    public ICollection<ProductResponsible> ProductResponsibles => _productResponsibles;
    private List<Asset> _assetOwners => new();
    public ICollection<Asset> AssetOwners => _assetOwners;
    private List<ConfigurationItem> _configurationItemOwners => new();
    public ICollection<ConfigurationItem> ConfigurationItemOwners => _configurationItemOwners;
    private List<AssetChangeOwnerHistory> _fromAssetChangeOwnerHistories => new();
    public ICollection<AssetChangeOwnerHistory> FromAssetChangeOwnerHistories => _fromAssetChangeOwnerHistories;
    private List<AssetChangeOwnerHistory> _toAssetChangeOwnerHistories => new();
    public ICollection<AssetChangeOwnerHistory> ToAssetChangeOwnerHistories => _toAssetChangeOwnerHistories;
    private List<ConfigurationItemChangeOwnerHistory> _toConfigurationItemChangeOwnerHistories => new();
    public ICollection<ConfigurationItemChangeOwnerHistory> ToConfigurationItemChangeOwnerHistories => _toConfigurationItemChangeOwnerHistories;
    private List<ConfigurationItemChangeOwnerHistory> _fromConfigurationItemChangeOwnerHistories => new();
    public ICollection<ConfigurationItemChangeOwnerHistory> FromConfigurationItemChangeOwnerHistories => _fromConfigurationItemChangeOwnerHistories;

    private List<BusinessContinuityStratgyResponsible> _businessContinuityStratgyResponsible = new();
    public ICollection<BusinessContinuityStratgyResponsible> BusinessContinuityStratgyResponsibles => _businessContinuityStratgyResponsible;


    private List<BusinessContinuityPlanResponsible> _businessContinuityPlanResponsible = new();
    public ICollection<BusinessContinuityPlanResponsible> BusinessContinuityPlanResponsibles => _businessContinuityPlanResponsible;

    private List<ChannelResponsible> _channelResponsibles = new();
    public ICollection<ChannelResponsible> ChannelResponsibles => _channelResponsibles;

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
