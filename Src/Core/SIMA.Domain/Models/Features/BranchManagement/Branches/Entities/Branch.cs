using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Args;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Exceptions;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.Branches.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

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
        if (arg.BranchChiefOfficerId.HasValue) BranchChiefOfficerId = arg.BranchChiefOfficerId;
        if (arg.BranchDeputyId.HasValue) BranchDeputyId = arg.BranchDeputyId;
        if (arg.LocationId.HasValue) LocationId = arg.LocationId;
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
        if (arg.BranchChiefOfficerId.HasValue) BranchChiefOfficerId = arg.BranchChiefOfficerId;
        if (arg.BranchDeputyId.HasValue) BranchDeputyId = arg.BranchDeputyId;
        Latitude = arg.Latitude;
        Longitude = arg.Longitude;
        if (arg.LocationId.HasValue) LocationId = arg.LocationId;
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

    public long? BranchChiefOfficerId { get; private set; }

    public long? BranchDeputyId { get; private set; }
    public DepartmentId DepartmentId { get; private set; }
    public virtual Department Department { get; private set; }

    public double? Latitude { get; private set; }

    public double? Longitude { get; private set; }

    public long? LocationId { get; private set; }

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
    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;
    }
    private async Task ModifyGuards(ModifyBranchArg arg, IBranchDomainService domainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (!arg.Longitude.HasValue || arg.Longitude == 0) throw SimaResultException.NullException;
        if (!arg.Latitude.HasValue || arg.Latitude == 0) throw SimaResultException.NullException;
        /// TODO MEHDI
        /*
         رئیس شعبه و معاون آن نمی تواند یک نفر باشند.
          یک فرد نمی تواند به صورت همزمان در دو شعبه متفاوت سمت داشته باشند.
          
         پرسنل خارج از دایره شهر انتخاب شده، نمی توانند به ریاست یا معاونت آن شعبه برگزیده شوند.

         */
        if (arg.Name.Length >= 200)
            throw SimaResultException.LengthNameException;

        if (arg.Code.Length >= 20)
            throw SimaResultException.LengthCodeException;

        if (await domainService.IsCodeUnique(arg.Code, arg.Id))
            throw SimaResultException.UniqueCodeError;

        if (!await domainService.IsNearExistingLocations(arg.Latitude.Value, arg.Longitude.Value))
            throw BranchExceptions.BranchDistanceException;
    }
    private static async Task CreateGuards(CreateBranchArg arg, IBranchDomainService domainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (!arg.Longitude.HasValue || arg.Longitude == 0) throw SimaResultException.NullException;
        if (!arg.Latitude.HasValue || arg.Latitude == 0) throw SimaResultException.NullException;
        /// TODO MEHDI
        /*
         رئیس شعبه و معاون آن نمی تواند یک نفر باشند.
          یک فرد نمی تواند به صورت همزمان در دو شعبه متفاوت سمت داشته باشند.
          
         پرسنل خارج از دایره شهر انتخاب شده، نمی توانند به ریاست یا معاونت آن شعبه برگزیده شوند.


         */
        if (arg.Name.Length >= 200)
            throw SimaResultException.LengthNameException;

        if (arg.Code.Length >= 20)
            throw SimaResultException.LengthCodeException;

        if (await domainService.IsCodeUnique(arg.Code, arg.Id))
            throw SimaResultException.UniqueCodeError;

        if (await domainService.IsNearExistingLocations(arg.Latitude.Value, arg.Longitude.Value))
            throw BranchExceptions.BranchDistanceException;
    }
}
