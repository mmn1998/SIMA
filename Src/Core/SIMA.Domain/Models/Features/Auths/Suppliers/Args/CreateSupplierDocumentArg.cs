namespace SIMA.Domain.Models.Features.Auths.Suppliers.Args;

public class CreateSupplierDocumentArg
{
    public long Id { get; set; }
    public long SupplierId { get; set; }
    public long DocumentId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
