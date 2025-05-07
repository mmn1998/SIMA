namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserLocationQueryForAggregate
{
    public long UserLocationId { get; set; }
    public long LocationId { get; set; }
    public string LocationName { get; set; }
    public long LocationTypeId { get; set; }
    public string LocationTypeName { get; set; }
}
