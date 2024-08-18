using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Positions.Entities;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Args;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Exceptions;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;

public class Branch : Entity
{
    private Branch()
    {

    }
    private Branch(CreateBranchArg arg)
    {
        Id = new BranchId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        Latitude = arg.Latitude;
        Longitude = arg.Longitude;
        PhoneNumber = arg.PhoneNumber;
        PostalCode = arg.PostalCode;
        Address = arg.Address;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        if (arg.BranchTypeId.HasValue) BranchTypeId = new BranchTypeId(arg.BranchTypeId.Value);
        if (arg.BranchChiefOfficerId.HasValue) BranchChiefOfficerId = new(arg.BranchChiefOfficerId.Value);
        if (arg.BranchDeputyId.HasValue) BranchDeputyId = new(arg.BranchDeputyId.Value);
        if (arg.LocationId.HasValue) LocationId = new(arg.LocationId.Value);
        if (arg.DepartmentId.HasValue) DepartmentId = new(arg.DepartmentId.Value);
    }
    public static async Task<Branch> Create(CreateBranchArg arg, IBranchDomainService domainService)
    {
        await CreateGuards(arg, domainService);
        return new Branch(arg);
    }
    public async Task Modify(ModifyBranchArg arg, IBranchDomainService domainService)
    {
        await ModifyGuards(arg, domainService);
        Name = arg.Name;
        Code = arg.Code;
        if (arg.BranchTypeId.HasValue) BranchTypeId = new BranchTypeId(arg.BranchTypeId.Value);
        if (arg.BranchChiefOfficerId.HasValue) BranchChiefOfficerId = new(arg.BranchChiefOfficerId.Value);
        if (arg.BranchDeputyId.HasValue) BranchDeputyId = new(arg.BranchDeputyId.Value);
        Latitude = arg.Latitude;
        Longitude = arg.Longitude;
        if (arg.LocationId.HasValue) LocationId = new(arg.LocationId.Value);
        PostalCode = arg.PostalCode;
        Address = arg.Address;
        PhoneNumber = arg.PhoneNumber;
        IsMultiCurrencyBranch = arg.IsMultiCurrencyBranch;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        if (arg.DepartmentId.HasValue) DepartmentId = new(arg.DepartmentId.Value);

    }
    public BranchId Id { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public BranchTypeId? BranchTypeId { get; private set; }

    public StaffId? BranchChiefOfficerId { get; private set; }
    public virtual Staff? BranchChiefOfficer { get; private set; }

    public StaffId? BranchDeputyId { get; private set; }
    public virtual Staff? BranchDeputy { get; private set; }
    public DepartmentId DepartmentId { get; private set; }
    public virtual Department Department { get; private set; }

    public double? Latitude { get; private set; }

    public double? Longitude { get; private set; }

    public LocationId? LocationId { get; private set; }
    public virtual Location? Location { get; private set; }

    public string? PhoneNumber { get; private set; }

    public string? PostalCode { get; private set; }

    public string? Address { get; private set; }

    public string? IsMultiCurrencyBranch { get; private set; }

    public long? ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual BranchType BranchType { get; private set; }
    private List<Position> _positions = new();
    public ICollection<Position> Positions => _positions;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private async Task ModifyGuards(ModifyBranchArg arg, IBranchDomainService domainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);

        if (await domainService.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);

        if (!arg.Longitude.HasValue || arg.Longitude == 0) throw new SimaResultException(CodeMessges._400Code,Messages.NullException);
        if (!arg.Latitude.HasValue || arg.Latitude == 0) throw new SimaResultException(CodeMessges._400Code,Messages.NullException);

        if (arg.BranchDeputyId.HasValue && arg.BranchChiefOfficerId.HasValue && (arg.BranchChiefOfficerId == arg.BranchDeputyId))
            throw new SimaResultException(CodeMessges._400Code , Messages.ChiefAndDeputyAreSameException);

        if (arg.BranchChiefOfficerId.HasValue && (!await domainService.IsStaffHasAnyRoleInOtherBrfanches(new(arg.BranchChiefOfficerId.Value), Id)))
            throw new SimaResultException(CodeMessges._400Code, Messages.StaffCantHaveRoleInTwoBranchesException);

        if (arg.BranchDeputyId.HasValue && (!await domainService.IsStaffHasAnyRoleInOtherBrfanches(new(arg.BranchDeputyId.Value), Id)))
            throw new SimaResultException(CodeMessges._400Code, Messages.StaffCantHaveRoleInTwoBranchesException);

        if (!await domainService.IsNearExistingLocations(arg.Latitude.Value, arg.Longitude.Value))
            throw new SimaResultException(CodeMessges._400Code, Messages.BranchDistanceException);
    }
    private static async Task CreateGuards(CreateBranchArg arg, IBranchDomainService domainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (!arg.Longitude.HasValue || arg.Longitude == 0) throw new SimaResultException(CodeMessges._400Code,Messages.NullException);
        if (!arg.Latitude.HasValue || arg.Latitude == 0) throw new SimaResultException(CodeMessges._400Code,Messages.NullException);

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);

        if (await domainService.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);

        if (arg.BranchDeputyId.HasValue && arg.BranchChiefOfficerId.HasValue && (arg.BranchChiefOfficerId == arg.BranchDeputyId))
            throw new SimaResultException(CodeMessges._400Code, Messages.ChiefAndDeputyAreSameException);

        if (arg.BranchChiefOfficerId.HasValue && (await domainService.IsStaffHasAnyRoleInOtherBrfanches(new(arg.BranchChiefOfficerId.Value))))
            throw new SimaResultException(CodeMessges._400Code, Messages.StaffCantHaveRoleInTwoBranchesException);

        if (arg.BranchDeputyId.HasValue && (await domainService.IsStaffHasAnyRoleInOtherBrfanches(new(arg.BranchDeputyId.Value))))
            throw new SimaResultException(CodeMessges._400Code, Messages.StaffCantHaveRoleInTwoBranchesException);

        if ((arg.BranchDeputyId.HasValue && arg.LocationId.HasValue) && !await domainService.IsStaffFromSelectedLocation(new(arg.BranchDeputyId.Value), new(arg.LocationId.Value)))
            throw new SimaResultException(CodeMessges._400Code, Messages.StaffShouldBeInSelectedException);


        if ((arg.BranchChiefOfficerId.HasValue && arg.LocationId.HasValue) && !await domainService.IsStaffFromSelectedLocation(new(arg.BranchChiefOfficerId.Value), new(arg.LocationId.Value)))
            throw new SimaResultException(CodeMessges._400Code, Messages.StaffShouldBeInSelectedException);

        if (await domainService.IsNearExistingLocations(arg.Latitude.Value, arg.Longitude.Value))
            throw new SimaResultException(CodeMessges._400Code, Messages.BranchDistanceException);
    }
}
