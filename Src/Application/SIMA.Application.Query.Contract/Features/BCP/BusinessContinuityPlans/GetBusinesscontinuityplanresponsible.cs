namespace SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityPlans;

public class GetBusinesscontinuityplanresponsible
{
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public long? PositionId { get; set; }
    public string? PositionName { get; set; }
    public long? PlanResponsibilityId { get; set; }
    public string? PlanResponsibilityName { get; set; }
    public string? IsForBackup { get; set; }
    public long DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public long CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public DateTime CreatedAt { get; set; }
}
