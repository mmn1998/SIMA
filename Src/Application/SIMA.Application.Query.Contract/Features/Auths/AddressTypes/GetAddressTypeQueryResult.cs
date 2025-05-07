namespace SIMA.Application.Query.Contract.Features.Auths.AddressTypes;

public class GetAddressTypeQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }
}
