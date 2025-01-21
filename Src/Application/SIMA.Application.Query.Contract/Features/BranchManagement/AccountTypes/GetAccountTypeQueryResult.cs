namespace SIMA.Application.Query.Contract.Features.BranchManagement.AccountTypes;

public class GetAccountTypeQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}