
namespace SIMA.Domain.Models.Features.BranchManagement.Branches.Args;

public class CreateBranchArg
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long? BranchTypeId { get; set; }
    public long? DepartmentId { get; set; }

    public long? BranchChiefOfficerId { get; set; }

    public long? BranchDeputyId { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public int? LocationId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? PostalCode { get; set; }

    public string? Address { get; set; }

    public string? IsMultiCurrencyBranch { get; set; }

    public int? ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
