namespace SIMA.Application.Query.Contract.Features.BranchManagement.FinancialSuppliers;

public class GetFinancialSupplierQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long? CustomerId { get; set; }
    public string? CustomerFullName { get; set; }
    public string? CustomerNumber { get; set; }
    public long? ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }
}
