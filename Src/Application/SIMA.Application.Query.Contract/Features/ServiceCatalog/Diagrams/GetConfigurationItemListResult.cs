namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetConfigurationItemListResult
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public List<Owner>? Owner { get; set; }
    public string? Title { get; set; }
    public string? VersionNumber { get; set; }
    public string? ReleaseDate { get; set; }
    public string? Description { get; set; }
    public List<DataCenter>? DataCenter { get; set; }
    public List<ConfigurationItemType>? ConfigurationItemType { get; set; }
    public List<ConfigurationItemStatus>? ConfigurationItemStatus { get; set; }
    public List<LicenseType>? LicenseType { get; set; }
    public List<LicenseSupplier>? LicenseSupplier { get; set; }
    public List<Supplier>? Supplier { get; set; }
    public List<CompanyBuildingLocation>? CompanyBuildingLocation { get; set; }
    public string? ActiveStatus { get; set; }
    public string? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
}

public class GetConfigurationItemListWrapperResult
{
    public List<GetConfigurationItemListResult>? Data { get; set; }
}

public class CompanyBuildingLocation
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class LicenseSupplier
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string Code { get; set; }
}

public class LicenseType
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class ConfigurationItemStatus
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class ConfigurationItemType
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}


