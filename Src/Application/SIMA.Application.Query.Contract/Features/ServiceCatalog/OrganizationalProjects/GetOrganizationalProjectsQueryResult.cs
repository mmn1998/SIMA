namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.OrganizationalProjects;

public class GetOrganizationalProjectsQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}
