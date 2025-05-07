namespace SIMA.Application.Query.Contract.Features.Auths.CustomerTypes;

public class GetCustomerTypeQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}