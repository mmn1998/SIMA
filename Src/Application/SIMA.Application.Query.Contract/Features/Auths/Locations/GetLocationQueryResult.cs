namespace SIMA.Application.Query.Contract.Features.Auths.Locations;

public class GetLocationQueryResult
{
    public long Id { get; set; }

    public string? LocationName { get; set; }

    public string? LocationCode { get; set; }

    public string LocationTypeName { get; set; }
    public string ParentLocationTypeName { get; set; }
    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }
}
