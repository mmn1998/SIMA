namespace SIMA.Application.Query.Contract.Features.Auths.Locations;

public class GetLocationQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ParentId { get; set; }
    public string? ParentName { get; set; }
    public string? LocationTypeName { get; set; }
    public string? ParentLocationTypeName { get; set; }
    public long ParentLocationTypeId { get; set; }
    public long LocationTypeId { get; set; }
    public long ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }
}
