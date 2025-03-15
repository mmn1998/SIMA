using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Departments.Args;
using SIMA.Domain.Models.Features.Auths.Departments.Interfaces;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Positions.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
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
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetAssignedStaffs.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;

namespace SIMA.Domain.Models.Features.Auths.Departments.Entities;

public class Department : Entity
{
    private Department()
    {

    }
    private Department(CreateDepartmentArg arg)
    {
        Id = new DepartmentId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        if (arg.ParentId.HasValue) ParentId = new DepartmentId(arg.ParentId.Value);
        if (arg.CompanyId.HasValue) CompanyId = new CompanyId(arg.CompanyId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        if (arg.LocationId.HasValue) LocationId = new LocationId(arg.LocationId.Value);
    }
    public static async Task<Department> Create(CreateDepartmentArg arg, IDepartmentService service)
    {
        await CreateGuards(arg, service);
        return new Department(arg);
    }

    public async Task Modify(ModifyDepartmentArg arg, IDepartmentService service)
    {
        await ModifyGuards(arg, service);
        Code = arg.Code;
        Name = arg.Name;
        if (arg.ParentId.HasValue) ParentId = new DepartmentId(arg.ParentId.Value);
        if (arg.CompanyId.HasValue) CompanyId = new CompanyId(arg.CompanyId.Value);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    #region Guards
    private static async Task CreateGuards(CreateDepartmentArg arg, IDepartmentService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        arg.Name.NullCheck();
        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyDepartmentArg arg, IDepartmentService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        arg.Name.NullCheck();
        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public DepartmentId Id { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public LocationId? LocationId { get; private set; }

    public DepartmentId? ParentId { get; private set; }

    public CompanyId? CompanyId { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual Company? Company { get; private set; }
    private List<Department> _departments = new();

    public ICollection<Department> InverseParent => _departments;

    public virtual Location? Location { get; private set; }

    public virtual Department? Parent { get; private set; }
    private List<Position> _positions = new();
    public ICollection<Position> Positions => _positions;
    private List<Branch> _branches = new();
    public ICollection<Branch> Branches => _branches;
    private List<Approval> _responsibleApprovals = new();
    public ICollection<Approval> ResponsibleApprovals => _responsibleApprovals;
    private List<Approval> _supervisorApprovals = new();
    public ICollection<Approval> SupervisorApprovals => _supervisorApprovals;
    private List<Invitees> _invitees = new();
    public ICollection<Invitees> Invitees => _invitees;

    private List<Service> _technicalSupervisorServices = new();
    public ICollection<Service> TechnicalSupervisorServices => _technicalSupervisorServices;

    private List<CriticalActivity> _criticalActivities = new();
    public ICollection<CriticalActivity> CriticalActivities => _criticalActivities;
    private List<Api> _apis = new();
    public ICollection<Api> OwnerApis => _apis;
    private List<CriticalActivityAssignedStaff> _criticalActivityAssignedStaffs = new();
    public ICollection<CriticalActivityAssignedStaff> CriticalActivityAssignedStaffs => _criticalActivityAssignedStaffs;
    private List<ServiceAssignedStaff> _serviceAssignedStaffs = new();
    public ICollection<ServiceAssignedStaff> ServiceAssignedStaffs => _serviceAssignedStaffs;
    private List<ChannelResponsible> _channelResponsibles = new();
    public ICollection<ChannelResponsible> ChannelResponsibles => _channelResponsibles;
    private List<ProductResponsible> _productResponsibles = new();
    public ICollection<ProductResponsible> ProductResponsibles => _productResponsibles;
    private List<BusinessImpactAnalysisStaff> _businessImpactAnalysisStaff = new();
    public ICollection<BusinessImpactAnalysisStaff> BusinessImpactAnalysisStaff => _businessImpactAnalysisStaff;
    private List<BusinessContinuityStratgyResponsible> _businessContinuityStratgyResponsibles = new();
    public ICollection<BusinessContinuityStratgyResponsible> BusinessContinuityStratgyResponsibles => _businessContinuityStratgyResponsibles;
    private List<BusinessContinuityPlanResponsible> _businessContinuityPlanResponsibles = new();
    public ICollection<BusinessContinuityPlanResponsible> BusinessContinuityPlanResponsibles => _businessContinuityPlanResponsibles;
    
    private List<AssetAssignedStaff> _assetAssignedStaffs = new();
    public ICollection<AssetAssignedStaff> AssetAssignedStaffs => _assetAssignedStaffs;

    private List<ApiSupportTeam> _apiSupportTeams = new();
    public ICollection<ApiSupportTeam> ApiSupportTeams => _apiSupportTeams;

    private List<DataProcedureSupportTeam> _dataProcedureSupportTeams = new();
    public ICollection<DataProcedureSupportTeam> DataProcedureSupportTeams => _dataProcedureSupportTeams;

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
