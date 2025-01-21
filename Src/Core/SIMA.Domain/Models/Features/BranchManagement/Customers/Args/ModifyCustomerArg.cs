namespace SIMA.Domain.Models.Features.BranchManagement.Customers.Args;

public class ModifyCustomerArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? CustomerNumber { get; set; }
    public long? ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
