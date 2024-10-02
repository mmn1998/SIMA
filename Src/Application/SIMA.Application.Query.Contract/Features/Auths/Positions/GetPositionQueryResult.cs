namespace SIMA.Application.Query.Contract.Features.Auths.Positions;

public class GetPositionQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? DepartmentName { get; set; }
    public long DepartmentId { get; set; }
    public string? CompanyName { get; set; }
    public long? CompanyId { get; set; }
    public long ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }
    public long PositionLevelId { get; set; }
    public string? PositionLevelName { get; set; }
    public long PositionTypeId { get; set; }
    public string? PositionTypeName { get; set; }
    public long BranchId { get; set; }
    public string? BranchName { get; set; }
    public int? PersonLimitation { get; set; }
}
