using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets;

public class GetAssetQueryResult
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string? SerialNumber { get; set; }
    public string? Title { get; set; }
    public string? Model { get; set; }
    public string? VersionNumber { get; set; }
    public string? Manufacturer { get; set; }
    public DateTime? ManufactureDate { get; set; }
    public string? ManufactureDatePersian => ManufactureDate.ToPersianDateTime(); 
    public DateTime? OwnerShipDate { get; set; }
    public string? OwnerShipDatePersian => OwnerShipDate.ToPersianDateTime();
    public DateTime? InserviceDate { get; set; }
    public string? InserviceDatePersian => InserviceDate.ToPersianDateTime();
    public DateTime? ExpireDate { get; set; }
    public string? ExpireDatePersian => ExpireDate.ToPersianDateTime();
    public DateTime? RetiredDate { get; set; }
    public string? RetiredDatePersian => ExpireDate.ToPersianDateTime();
    public string? Description { get; set; }

    public decimal? OwnershipPaymentValue { get; set; }
    public decimal? OwnershipPrepaymentValue { get; set; }

    public string? HasConfidentialInformation { get; set; }
   
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? CreatedBy { get; set; }
    public string? ActiveStatus { get; set; }
    
    public long? IssueId { get; set; }
    public string? IssueCode { get; set; }
    public long? WorkFlowId { get; set; }
    public string? WorkFlowName { get; set; }
    public long? CurrentStateId { get; set; }
    public string? CurrentStateName { get; set; }
    public long? CurrentStepId { get; set; }
    public string? CurrentStepName { get; set; }
    public DateTime? IssueCreatedAt { get; set; }
    public string? IssueCreatedBy { get; set; }

}