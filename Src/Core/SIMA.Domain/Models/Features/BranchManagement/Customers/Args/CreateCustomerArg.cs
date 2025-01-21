namespace SIMA.Domain.Models.Features.BranchManagement.Customers.Args;

public class CreateCustomerArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? CustomerNumber { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}