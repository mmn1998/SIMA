namespace SIMA.Application.Query.Contract.Features.Auths.Departments;

public class GetDepartmentQueryResult
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? ParentName { get; set; }
    public long? ParentId { get; set; }
    public string? CompanyName { get; set; }
    public long? CompanyId { get; set; }
    public long ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }
}
