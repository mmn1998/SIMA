namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetConfigurationItemListResult
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public Owner? Owner { get; set; }
    public string? Title { get; set; }
    public string? VersionNumber { get; set; }
    public string? ReleaseDate { get; set; }
    public string? Description { get; set; }
    public DataCenter? DataCenter { get; set; }
    public ConfigurationItemType? ConfigurationItemType { get; set; }
    public ConfigurationItemStatus? ConfigurationItemStatus { get; set; }
    public LicenseType? LicenseType { get; set; }
    public LicenseSupplier? LicenseSupplier { get; set; }
    public Supplier? Supplier { get; set; }
    public CompanyBuildingLocation? CompanyBuildingLocation { get; set; }
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


