using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.Assets;

public class CreateAssetCommand : ICommand<Result<long>>
{
    public string? Code { get; set; }
    public string? SerialNumber { get; set; }
    public long? SupplierId { get; set; }
    public long? OwnerId { get; set; }
    public long? WarehouseId { get; set; }
    public long? AssetTypeId { get; set; }
    public long? AssetCategoryId { get; set; }
    public string? Title { get; set; }
    public string? Model { get; set; }
    public string? VersionNumber { get; set; }
    public string? Manufacturer { get; set; }
    public string? ManufactureDate { get; set; }
    public string? OwnershipDate { get; set; }
    public string? InServiceDate { get; set; }
    public string? ExpireDate { get; set; }
    public string? RetiredDate { get; set; }
    public string? Description { get; set; }
    public long? DataCenterId { get; set; }
    public long? OperationalStatusId { get; set; }
    public long? AssetTechnicalStatusId { get; set; }
    public long? AssetPhysicalStatusId { get; set; }
    public long? OwnershipTypeId { get; set; }
    public decimal? OwnershipPrepaymentValue { get; set; }
    public decimal? OwnershipPaymentValue { get; set; }
    public long? UserTypeId { get; set; }
    public long? BusinessCriticalityId { get; set; }
    public long PhysicalLocationId { get; set; }
    public string? HasConfidentialInformation { get; set; }

   

    public ICollection<CreateAssetCustomFeildValueCommand>? AssetCustomFeildValueList { get; set; }
    public ICollection<CreateAssetCustomFeildOptionCommand>? AssetCustomFeildOptionList { get; set; }
    public ICollection<long>? ServiceAssetList { get; set; }
    public ICollection<long>? AssetDocumentList { get; set; }
    public ICollection<CreateAssetAssignedStaffCommand>? AssetAssignedStaffList { get; set; }
    public ICollection<long>? ConfigurationItemAssetList { get; set; }
    public ICollection<long>? ComplexAssetList { get; set; }
}