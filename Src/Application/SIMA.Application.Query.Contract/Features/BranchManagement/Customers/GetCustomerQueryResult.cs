namespace SIMA.Application.Query.Contract.Features.BranchManagement.Customers;

public class GetCustomerQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? CustomerNumber { get; set; }
    public string? ActiveStatus { get; set; }
}