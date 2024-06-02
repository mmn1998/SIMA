namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCustomerTypes.Args;

public class ModifyServiceCustomerTypeArg
{
    public long Id { get;  set; }
    public string Name { get;  set; }
    public string Code { get;  set; }
    public long? ParentId { get;  set; }
    public long? ActiveStatusId { get;  set; }
    public byte[]? ModifiedAt { get;  set; }
    public long? ModifiedBy { get;  set; }
}
