namespace SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Args;

public class CreateApiTypeArg
{
    public long Id { get; set; }
    public string Name { get;  set; }
    public string Code { get;  set; }
    public long ActiveStatusId { get;  set; }
    public DateTime CreatedAt { get;  set; }
    public long CreatedBy { get;  set; }
}