using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.ChannelTypeCatalogs.ChannelTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceBoundles.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriority.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.NewFolder;
namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

public class Service : Entity
{
    private Service()
    {

    }
    private Service(CreateServiceArg arg)
    {
        Id = new ServiceId(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ParentId = arg.ParentId;
        ServiceCost = arg.ServiceCost;
        Description = arg.Description;
        ServiceWorkflowDescription = arg.ServiceWorkflowDescription;
        ContinuousImprovement = arg.ContinuousImprovement;
        Suggestion = arg.Suggestion;
        FeedbackUrl = arg.FeedbackUrl;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<Service> Create(CreateServiceArg arg)
    {
        return new Service(arg);
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public ServiceId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long? ParentId { get; private set; }
    public double? ServiceCost { get; private set; }
    public string? Description { get; private set; }
    public string? ServiceWorkflowDescription { get; private set; }
    public string? ContinuousImprovement { get; private set; }
    public string? Suggestion { get; private set; }
    public string? FeedbackUrl { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    public virtual ServiceBoundle ServiceBoundle { get; private set; }
    public ServiceBoundleId? ServiceBoundleId { get; private set; }

    public virtual Staff StaffTechnicalResponsible { get; private set; }
    public StaffId TechnicalResponsibleId { get; private set; }
    public virtual Staff StaffBusinessResponsible { get; private set; }
    public StaffId BusinessResponsibleId { get; private set; }
    public virtual Staff StaffTechnicalSupport { get; private set; }
    public StaffId TechnicalSupportId { get; private set; }
    public virtual Staff StaffTechnicalSupervisor { get; private set; }
    public StaffId TechnicalSupervisorId { get; private set; }


    public virtual ServicePriority.Entities.ServicePriority ServicePriority { get; private set; }
    public ServicePriorityId ServicePriorityId { get; private set; }

    public virtual Department OwnerDepartment { get; private set; }
    public DepartmentId? OwnerDepartmentId { get; private set; }

    public virtual Department TechnicalSupervisorDepartment { get; private set; }
    public DepartmentId? TechnicalSupervisorDepartmentId { get; private set; }


    private List<ServiceChannel> _serviceChanneles = new();
    public ICollection<ServiceChannel> ServiceChanneles => _serviceChanneles;

    private List<ServiceDocument> _serviceDocuments = new();
    public ICollection<ServiceDocument> ServiceDocuments => _serviceDocuments;

    private List<ServiceProvider> _serviceProviders = new();
    public ICollection<ServiceProvider> ServiceProviders => _serviceProviders;

    private List<ServiceAssignStaff> _serviceAssignStaffes = new();
    public ICollection<ServiceAssignStaff> ServiceAssignStaffes => _serviceAssignStaffes;

    private List<CriticalActivityRisk> _criticalActivityRisks = new();
    public ICollection<CriticalActivityRisk> CriticalActivityRisks => _criticalActivityRisks;

    private List<ServiceAsset> _serviceAssets = new();
    public ICollection<ServiceAsset> ServiceAssets => _serviceAssets;

    private List<ServiceCantract> _serviceCantracts = new();
    public ICollection<ServiceCantract> ServiceCantracts => _serviceCantracts;

    private List<ServiceCustomer> _serviceCustomers = new();
    public ICollection<ServiceCustomer> ServiceCustomers => _serviceCustomers;

    //private List<ServiceRisk> _serviceRisks = new();
    //public ICollection<ServiceRisk> ServiceRisks => _serviceRisks;

    private List<ServiceUser> _serviceUsers = new();
    public ICollection<ServiceUser> ServiceUsers => _serviceUsers;
    private List<ServiceApi> _serviceApis = new();
    public ICollection<ServiceApi> ServiceApis => _serviceApis;
    private List<CriticalActivityService> _criticalActivityServices = new();
    public ICollection<CriticalActivityService> CriticalActivityServices => _criticalActivityServices;

    private List<ServiceRelatedIssue> _serviceRelatedIssues = new();
    public ICollection<ServiceRelatedIssue> ServiceRelatedIssues => _serviceRelatedIssues;
    
    private List<ServiceAvalibility> _serviceAvalibilities = new();
    public ICollection<ServiceAvalibility> ServiceAvalibilities => _serviceAvalibilities;
    
    private List<PreRequisiteServices> _requiredServicess = new();
    public ICollection<PreRequisiteServices> RequiredServicess => _requiredServicess;
    private List<PreRequisiteServices> _preRequisiteServicess = new();
    public ICollection<PreRequisiteServices> PreRequisiteServicess => _preRequisiteServicess;

}
