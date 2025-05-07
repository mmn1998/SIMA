namespace SIMA.Application.Query.Contract.Features.BranchManagement.Branches;

public class GetBranchQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long? BranchTypeId { get; set; }

    public long? BranchChiefOfficerId { get; set; }

    public long? BranchDeputyId { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public long? LocationId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? PostalCode { get; set; }

    public string? Address { get; set; }

    public string? IsMultiCurrencyBranch { get; set; }
    public string? ActiveStatus { get; set; }
    public long? ActiveStatusId { get; set; }
}
