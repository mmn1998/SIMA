namespace SIMA.Domain.Models.Features.Auths.Departments.Args;

public class CreateDepartmentArg
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public long? LocationId { get; set; }

    public long? ParentId { get; set; }

    public long? CompanyId { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }

}
