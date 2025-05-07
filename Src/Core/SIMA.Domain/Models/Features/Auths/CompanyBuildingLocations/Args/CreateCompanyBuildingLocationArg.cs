namespace SIMA.Domain.Models.Features.Auths.CompanyBuildingLocations.Args;

public class CreateCompanyBuildingLocationArg
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
    public long CompanyId { get; set; }
    public long LocationId { get; set; }
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
}