namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Args;

public class CreateServiceCategoryArg
{
    public long Id { get; set; }
    public long ServiceTypeId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}