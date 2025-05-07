namespace SIMA.Application.Contract.Features.ServiceCatalog.Products;

public class ProductResponsibleCommand
{
    public long? ResponsibleTypeId { get; set; }
    public long? ResponsibleId { get; set; }
    public long? DepartmentId { get; set; }
    public long? BranchId { get; set; }
}
