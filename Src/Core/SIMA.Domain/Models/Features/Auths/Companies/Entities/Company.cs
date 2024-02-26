using SIMA.Domain.Models.Features.Auths.Companies.Args;
using SIMA.Domain.Models.Features.Auths.Companies.Interfaces;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

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

    public async void Modify(ModifyCompanyArg arg, ICompanyService service)
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

        if (arg.Name.Length > 200) throw SimaResultException.LengthNameException;
        if (arg.Code.Length > 20) throw SimaResultException.LengthCodeException;
        if (!await service.IsCodeUnique(arg.Code, 0)) throw SimaResultException.UniqueCodeError;
        if (await service.IsCompanyParent(arg.ParentId)) throw SimaResultException.ParentCompaniesCannotDefinedAsChildError;
    }
    private async Task ModifyGuards(ModifyCompanyArg arg, ICompanyService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw SimaResultException.LengthNameException;
        if (arg.Code.Length > 20) throw SimaResultException.LengthCodeException;
        if (!await service.IsCodeUnique(arg.Code, 0)) throw SimaResultException.UniqueCodeError;
        if (await service.IsCompanyParent(arg.ParentId)) throw SimaResultException.ParentCompaniesCannotDefinedAsChildError;
    }
    #endregion
    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
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

    private List<Department> _department = new();

    public virtual Company Companies { get; set; }
    public ICollection<Department> Departments => _department;

    private List<User> _users = new();
    public ICollection<User> Users => _users;

    //private List<Company> _companies = new();

    //public virtual ICollection<Company> InverseParent=> _companies;

    public virtual Company? Parent { get; set; }
}
