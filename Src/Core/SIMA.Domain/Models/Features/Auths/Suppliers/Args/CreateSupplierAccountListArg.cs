namespace SIMA.Domain.Models.Features.Auths.Suppliers.Args;

public class CreateSupplierAccountListArg
{
    public long Id { get; set; }
    public long SupplierId { get;  set; }
    public string IBAN { get;  set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get;  set; }
    public long? CreatedBy { get;  set; }
}
