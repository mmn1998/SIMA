namespace SIMA.Domain.Models.Features.Logistics.Suppliers.Args;

public class ModifySupplierArg
{
    public long SupplierRankId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MobileNumber { get; set; }
    public string? FaxNumber { get; set; }
    public string? PostalCode { get; set; }
    public string? Address { get; set; }
    public string? IsInBlackList { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
