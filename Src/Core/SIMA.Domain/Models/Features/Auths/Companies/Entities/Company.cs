using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Args;
using SIMA.Domain.Models.Features.Auths.Companies.Args;
using SIMA.Domain.Models.Features.Auths.Companies.Interfaces;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.CompanyBuildingLocations.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Companies.Entities;

public class Company : Entity
{
    private Company()
    {

    }
    private Company(CreateCompanyArg arg)
    {
        Id = new CompanyId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        if (arg.ParentId.HasValue) ParentId = new CompanyId(arg.ParentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        if (arg.ParentId.HasValue) ParentId = new CompanyId(arg.ParentId.Value);

    }
    public static async Task<Company> Create(CreateCompanyArg arg, ICompanyService service)
    {
        await CreateGuards(arg, service);
        return new Company(arg);
    }

    public async Task Modify(ModifyCompanyArg arg, ICompanyService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        if (arg.ParentId.HasValue) ParentId = new CompanyId(arg.ParentId.Value);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    #region Guards
    private static async Task CreateGuards(CreateCompanyArg arg, ICompanyService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (await service.IsCompanyParent(arg.ParentId)) throw new SimaResultException(CodeMessges._100022Code, Messages.ParentCompaniesCannotDefinedAsChildError);
    }
    private async Task ModifyGuards(ModifyCompanyArg arg, ICompanyService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (await service.IsCompanyParent(arg.ParentId)) throw new SimaResultException(CodeMessges._100022Code, Messages.ParentCompaniesCannotDefinedAsChildError);
    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public CompanyId Id { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public CompanyId? ParentId { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public virtual Company? Parent { get; set; }

    private List<Department> _department = new();

    public virtual Company Companies { get; set; }
    public ICollection<Department> Departments => _department;

    private List<User> _users = new();
    public ICollection<User> Users => _users;

    private List<WorkFlowCompany> _workFlowCompanies = new();
    public virtual ICollection<WorkFlowCompany> WorkFlowCompanies => _workFlowCompanies;

    private List<Approval> _responsibleApprovals = new();
    public ICollection<Approval> ResponsibleApprovals => _responsibleApprovals;

    private List<Approval> _supervisorApprovals = new();
    public ICollection<Approval> SupervisorApprovals => _supervisorApprovals;

    private List<Invitees> _invitees = new();
    public ICollection<Invitees> Invitees => _invitees;

    private List<ServiceProvider> _serviceProviders = new();
    public ICollection<ServiceProvider> ServiceProviders => _serviceProviders;
    private List<CompanyBuildingLocation> _companyBuildingLocations = new();

    public ICollection<CompanyBuildingLocation> CompanyBuildingLocations => _companyBuildingLocations;

    private List<Product> _products = new();
    public ICollection<Product> Products => _products;
}
