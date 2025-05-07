namespace SIMA.Application.Query.Contract.Features.Auths.Companies;

public class GetCompanyQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ParentName { get; set; }
    public long? ParentId { get; set; }
    public long ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }

}
