using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Domain.Models.Features.Auths.CompanyBuildingLocations.Contracts;
using SIMA.Domain.Models.Features.Auths.CompanyBuildingLocations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Args;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.CompanyBuildingLocations.Entities;

public class CompanyBuildingLocation : Entity, IAggregateRoot
{
    private CompanyBuildingLocation()
    {

    }
    private CompanyBuildingLocation(CreateCompanyBuildingLocationArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        Code = arg.Code;
        LocationId = new(arg.LocationId);
        CompanyId = new(arg.CompanyId);
        Fax = arg.Fax;
        PostalCode = arg.PostalCode;
        Address = arg.Address;
        Phone = arg.Phone;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<CompanyBuildingLocation> Create(CreateCompanyBuildingLocationArg arg, ICompanyBuildingLocationDomainService service)
    {
        await CreateGuards(arg, service);
        return new CompanyBuildingLocation(arg);
    }
    public async Task Modify(ModifyCompanyBuildingLocationArg arg, ICompanyBuildingLocationDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        Code = arg.Code;
        Fax = arg.Fax;
        PostalCode = arg.PostalCode;
        Address = arg.Address;
        Phone = arg.Phone;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateCompanyBuildingLocationArg arg, ICompanyBuildingLocationDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyCompanyBuildingLocationArg arg, ICompanyBuildingLocationDomainService service)
    {

    }
    #endregion
    public CompanyBuildingLocationId Id { get; private set; }
    public string? Name { get; private set; }

    public string? Code { get; private set; }
    public CompanyId CompanyId { get; private set; }
    public virtual Company Company { get; private set; }
    public LocationId LocationId { get; private set; }
    public virtual Location Location { get; private set; }
    public string? Address { get; private set; }
    public string? PostalCode { get; private set; }
    public string? Phone { get; private set; }
    public string? Fax { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<Warehouse> _warehouses = new();
    public ICollection<Warehouse> Warehouses => _warehouses;
}