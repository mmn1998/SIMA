namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;

public class GetServiceUserQueryResult
{
    public long UserTypeId { get; set; }
    public string? UserTypeName { get; set; }
}public class GetServiceOrganizationProjectQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
}
