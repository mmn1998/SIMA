using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Departments.Args;
using SIMA.Domain.Models.Features.Auths.Departments.Interfaces;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Positions.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

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
        return new Department(arg);
    }

    public void Modify(ModifyDepartmentArg arg, IDepartmentService service)
    {
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
        if (!await service.IsCodeUnique(arg.Code, 0)) throw SimaResultException.UniqueCodeError;
    }
    private async Task ModifyGuards(ModifyDepartmentArg arg, IDepartmentService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        arg.Name.NullCheck();
        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw SimaResultException.UniqueCodeError;
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
    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }
}
