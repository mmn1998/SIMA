namespace SIMA.Application.Query.Contract.Features.Auths.LocationTypes;

public class GetLocationTypeQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ParentId { get; set; }
    public string ParentName { get; set; }
    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }

}
