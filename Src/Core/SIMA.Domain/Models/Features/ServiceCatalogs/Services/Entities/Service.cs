using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
public class Service : Entity, IAggregateRoot
{
    private Service()
    {

    }
    private Service(CreateServiceArg arg)
    {
        Id = new ServiceId(arg.Id);
        ServiceBoundleId = new ServiceBoundleId(arg.ServiceBoundleId);
        ServiceStatusId = arg.ServiceStatusId.HasValue ? new(arg.ServiceStatusId.Value) : null;
        TechnicalSupervisorDepartmentId = arg.TechnicalSupervisorDepartmentId.HasValue ? new DepartmentId(arg.TechnicalSupervisorDepartmentId.Value) : null;
        Name = arg.Name;
        Code = arg.Code;
        ServiceCost = arg.ServiceCost;
        Description = arg.Description;
        ServiceWorkflowDescription = arg.ServiceWorkflowDescription;
        ContinuousImprovement = arg.ContinuousImprovement;
        Suggestion = arg.Suggestion;
        FeedbackUrl = arg.FeedbackUrl;
        ActiveStatusId = arg.ActiveStatusId;
        InServiceDate = arg.InServiceDate;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<Service> Create(CreateServiceArg arg, IServiceDomainService service)
    {
        await CreateGuards(arg, service);
        return new Service(arg);
    }
    public async Task Modify(ModifyServiceArg arg, IServiceDomainService service)
    {
        await ModifyGuards(arg, service);
        ServiceBoundleId = new ServiceBoundleId(arg.ServiceBoundleId);
        TechnicalSupervisorDepartmentId = arg.TechnicalSupervisorDepartmentId.HasValue ? new(arg.TechnicalSupervisorDepartmentId.Value) : null;
        ServiceStatusId = arg.ServiceStatusId.HasValue ? new(arg.ServiceStatusId.Value) : null;
        Name = arg.Name;
        Code = arg.Code;
        ServiceCost = arg.ServiceCost;
        Description = arg.Description;
        ServiceWorkflowDescription = arg.ServiceWorkflowDescription;
        ContinuousImprovement = arg.ContinuousImprovement;
        Suggestion = arg.Suggestion;
        FeedbackUrl = arg.FeedbackUrl;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        InServiceDate = arg.InServiceDate;
    }
    #region Guards
    private static async Task CreateGuards(CreateServiceArg arg, IServiceDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyServiceArg arg, IServiceDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        #region DeleteRelatedEntities
        DeleteServiceAssets(userId);
        DeleteServiceAssignedStaffs(userId);
        DeleteServiceChannels(userId);
        DeleteServiceConfigurationItems(userId);
        DeleteServiceCustomers(userId);
        DeleteServicePrerequisites(userId);
        DeleteServiceProviders(userId);
        DeleteServiceRisks(userId);
        DeleteServiceUsers(userId);
        #endregion
    }
    #region AddMethods
    public void AddServiceUsers(List<CreateServiceUserArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ServiceUser.Create(arg);
            _serviceUsers.Add(entity);
        }
    }
    public void AddServiceCustomers(List<CreateServiceCustomerArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ServiceCustomer.Create(arg);
            _serviceCustomers.Add(entity);
        }
    }
    public void AddServiceChannels(List<CreateServiceChannelArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ServiceChannel.Create(arg);
            _serviceChanneles.Add(entity);
        }
    }
    public void AddServicePrerequisite(List<CreatePreRequisiteServicesArg> args)
    {
        foreach (var arg in args)
        {
            var entity = PreRequisiteServices.Create(arg);
            _preRequisiteServicess.Add(entity);
        }
    }
    public void AddServiceProviders(List<CreateServiceProviderArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ServiceProvider.Create(arg);
            _serviceProviders.Add(entity);
        }
    }
    public void AddServiceRisks(List<CreateServiceRiskArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ServiceRisk.Create(arg);
            _serviceRisks.Add(entity);
        }
    }
    public void AddServiceAssets(List<CreateServiceAssetArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ServiceAsset.Create(arg);
            _serviceAssets.Add(entity);
        }
    }
    public void AddServiceConfigurationItems(List<CreateServiceConfigurationItemArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ServiceConfigurationItem.Create(arg);
            _serviceConfigurationItems.Add(entity);
        }
    }
    public void AddServiceAssignedStaffs(List<CreateServiceAssignedStaffArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ServiceAssignedStaff.Create(arg);
            _serviceAssignStaffes.Add(entity);
        }
    }
    #endregion
    #region DeleteMethods
    public void DeleteServiceUsers(long userId)
    {
        foreach (var item in _serviceUsers)
        {
            item.Delete(userId);
        }
    }
    public void DeleteServiceCustomers(long userId)
    {
        foreach (var item in _serviceCustomers)
        {
            item.Delete(userId);
        }
    }
    public void DeleteServiceChannels(long userId)
    {
        foreach (var item in _serviceChanneles)
        {
            item.Delete(userId);
        }
    }
    public void DeleteServicePrerequisites(long userId)
    {
        foreach (var item in _preRequisiteServicess)
        {
            item.Delete(userId);
        }
    }
    public void DeleteServiceProviders(long userId)
    {
        foreach (var item in _serviceProviders)
        {
            item.Delete(userId);
        }
    }
    public void DeleteServiceRisks(long userId)
    {
        foreach (var item in _serviceRisks)
        {
            item.Delete(userId);
        }
    }
    public void DeleteServiceAssets(long userId)
    {
        foreach (var item in _serviceAssets)
        {
            item.Delete(userId);
        }
    }
    public void DeleteServiceConfigurationItems(long userId)
    {
        foreach (var item in _serviceConfigurationItems)
        {
            item.Delete(userId);
        }
    }
    public void DeleteServiceAssignedStaffs(long userId)
    {
        foreach (var item in _serviceAssignStaffes)
        {
            item.Delete(userId);
        }
    }
    #endregion
    #region ModifyMethods
    public void ModifyServiceUsers(List<CreateServiceUserArg> args)
    {
        var activeEntities = _serviceUsers.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ServiceUserTypeId == x.ServiceUserTypeId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ServiceUserTypeId.Value == x.ServiceUserTypeId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _serviceUsers.FirstOrDefault(x => x.ServiceUserTypeId.Value == arg.ServiceUserTypeId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ServiceUser.Create(arg);
                _serviceUsers.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyServiceCustomers(List<CreateServiceCustomerArg> args)
    {
        var activeEntities = _serviceCustomers.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ServiceCustomerTypeId == x.ServiceCustomerTypeId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ServiceCustomerTypeId.Value == x.ServiceCustomerTypeId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _serviceCustomers.FirstOrDefault(x => x.ServiceCustomerTypeId.Value == arg.ServiceCustomerTypeId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ServiceCustomer.Create(arg);
                _serviceCustomers.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyServiceChannels(List<CreateServiceChannelArg> args)
    {
        var activeEntities = _serviceChanneles.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ChannelTypeId == x.ChannelTypeId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ChannelTypeId.Value == x.ChannelTypeId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _serviceChanneles.FirstOrDefault(x => x.ChannelTypeId.Value == arg.ChannelTypeId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ServiceChannel.Create(arg);
                _serviceChanneles.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyServicePrerequisite(List<CreatePreRequisiteServicesArg> args)
    {
        var activeEntities = _preRequisiteServicess.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.PreRequiredServiceId == x.PreRequiredServiceId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.PreRequiredServiceId.Value == x.PreRequiredServiceId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _preRequisiteServicess.FirstOrDefault(x => x.PreRequiredServiceId.Value == arg.PreRequiredServiceId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = PreRequisiteServices.Create(arg);
                _preRequisiteServicess.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyServiceProviders(List<CreateServiceProviderArg> args)
    {
        var activeEntities = _serviceProviders.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.CompanyId == x.CompanyId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.CompanyId.Value == x.CompanyId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _serviceProviders.FirstOrDefault(x => x.CompanyId.Value == arg.CompanyId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ServiceProvider.Create(arg);
                _serviceProviders.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyServiceRisks(List<CreateServiceRiskArg> args)
    {
        var activeEntities = _serviceRisks.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.RiskId == x.RiskId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.RiskId.Value == x.RiskId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _serviceRisks.FirstOrDefault(x => x.RiskId.Value == arg.RiskId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ServiceRisk.Create(arg);
                _serviceRisks.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyServiceAssets(List<CreateServiceAssetArg> args)
    {
        var activeEntities = _serviceAssets.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.AssetId == x.AssetId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.AssetId.Value == x.AssetId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _serviceAssets.FirstOrDefault(x => x.AssetId.Value == arg.AssetId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ServiceAsset.Create(arg);
                _serviceAssets.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyServiceConfigurationItems(List<CreateServiceConfigurationItemArg> args)
    {
        var activeEntities = _serviceConfigurationItems.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ConfigurationItemId == x.ConfigurationItemId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ConfigurationItemId.Value == x.ConfigurationItemId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _serviceConfigurationItems.FirstOrDefault(x => x.ConfigurationItemId.Value == arg.ConfigurationItemId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ServiceConfigurationItem.Create(arg);
                _serviceConfigurationItems.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyServiceAssignedStaffs(List<CreateServiceAssignedStaffArg> args)
    {
        var activeEntities = _serviceAssignStaffes.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.StaffId == x.StaffId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.StaffId.Value == x.StaffId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _serviceAssignStaffes.FirstOrDefault(x => x.StaffId.Value == arg.StaffId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ServiceAssignedStaff.Create(arg);
                _serviceAssignStaffes.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    #endregion
    public ServiceId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long? ParentId { get; private set; }
    public double? ServiceCost { get; private set; }
    public string? Description { get; private set; }
    public string? ServiceWorkflowDescription { get; private set; }
    public string? ContinuousImprovement { get; private set; }
    public string? Suggestion { get; private set; }
    public string? FeedbackUrl { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    public virtual ServiceBoundle ServiceBoundle { get; private set; }
    public ServiceBoundleId ServiceBoundleId { get; private set; }

    public virtual ServiceStatus ServiceStatus { get; private set; }
    public ServiceStatusId? ServiceStatusId { get; private set; }
    public DateOnly? InServiceDate { get; private set; }
    public string? IsCriticalService { get; private set; }


    public virtual Department TechnicalSupervisorDepartment { get; private set; }
    public DepartmentId? TechnicalSupervisorDepartmentId { get; private set; }

    private List<ServiceChannel> _serviceChanneles = new();
    public ICollection<ServiceChannel> ServiceChanneles => _serviceChanneles;

    private List<ServiceDocument> _serviceDocuments = new();
    public ICollection<ServiceDocument> ServiceDocuments => _serviceDocuments;

    private List<ServiceProvider> _serviceProviders = new();
    public ICollection<ServiceProvider> ServiceProviders => _serviceProviders;

    private List<ServiceAssignedStaff> _serviceAssignStaffes = new();
    public ICollection<ServiceAssignedStaff> ServiceAssignStaffes => _serviceAssignStaffes;

    private List<CriticalActivityRisk> _criticalActivityRisks = new();
    public ICollection<CriticalActivityRisk> CriticalActivityRisks => _criticalActivityRisks;

    private List<ServiceAsset> _serviceAssets = new();
    public ICollection<ServiceAsset> ServiceAssets => _serviceAssets;
    private List<ServiceRisk> _serviceRisks = new();
    public ICollection<ServiceRisk> ServiceRisks => _serviceRisks;

    private List<ServiceContract> _serviceCantracts = new();
    public ICollection<ServiceContract> ServiceCantracts => _serviceCantracts;

    private List<ServiceCustomer> _serviceCustomers = new();
    public ICollection<ServiceCustomer> ServiceCustomers => _serviceCustomers;

    private List<ServiceConfigurationItem> _serviceConfigurationItems = new();
    public ICollection<ServiceConfigurationItem> ServiceConfigurationItems => _serviceConfigurationItems;

    private List<ServiceUser> _serviceUsers = new();
    public ICollection<ServiceUser> ServiceUsers => _serviceUsers;
    private List<ServiceApi> _serviceApis = new();
    public ICollection<ServiceApi> ServiceApis => _serviceApis;
    private List<CriticalActivityServices> _criticalActivityServices = new();
    public ICollection<CriticalActivityServices> CriticalActivityServices => _criticalActivityServices;

    private List<ServiceRelatedIssue> _serviceRelatedIssues = new();
    public ICollection<ServiceRelatedIssue> ServiceRelatedIssues => _serviceRelatedIssues;

    private List<ServiceAvalibility> _serviceAvalibilities = new();
    public ICollection<ServiceAvalibility> ServiceAvalibilities => _serviceAvalibilities;

    private List<PreRequisiteServices> _requiredServicess = new();
    public ICollection<PreRequisiteServices> RequiredServicess => _requiredServicess;
    private List<PreRequisiteServices> _preRequisiteServicess = new();
    public ICollection<PreRequisiteServices> PreRequisiteServicess => _preRequisiteServicess;
    private List<BusinessImpactAnalysis> _businessImpactAnalyses = new();
    public ICollection<BusinessImpactAnalysis> BusinessImpactAnalyses => _businessImpactAnalyses;
    private List<BusinessContinuityStrategyService> _businessContinuityStrategyServices = new();
    public ICollection<BusinessContinuityStrategyService> BusinessContinuityStrategyServices => _businessContinuityStrategyServices
        ; private List<BusinessContinuityPlanService> _businessContinuityPlanServices = new();
    public ICollection<BusinessContinuityPlanService> BusinessContinuityPlanServices => _businessContinuityPlanServices;

}
