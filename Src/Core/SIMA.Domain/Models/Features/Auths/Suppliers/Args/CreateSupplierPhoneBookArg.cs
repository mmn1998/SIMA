namespace SIMA.Domain.Models.Features.Auths.Suppliers.Args;

public class CreateSupplierPhoneBookArg
{
    public long Id { get; set; }
    public long SupplierId { get; set; }
    public long PhoneTypeId { get; set; }
    public string PhoneNumber { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}