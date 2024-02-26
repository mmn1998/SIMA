namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class PositionResult
{
    public string? Department { get; set; }
    public string? DepartmentCode { get; set; }
    public string? Position { get; set; }
    public string? PositionCode { get; set; }
    public string? Manager { get; set; }
    public long? ManagerId { get; set; }
    public string? ActiveFrom { get; set; }
    public string? ActiveTo { get; set; }
}
