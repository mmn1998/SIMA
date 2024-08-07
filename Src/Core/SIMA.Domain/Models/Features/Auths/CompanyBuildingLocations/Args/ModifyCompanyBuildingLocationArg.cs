namespace SIMA.Domain.Models.Features.Auths.OwnershipTypes.Args;

public class ModifyCompanyBuildingLocationArg
{
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
    public long CompanyId { get; set; }
    public long LocationId { get; set; }
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
}