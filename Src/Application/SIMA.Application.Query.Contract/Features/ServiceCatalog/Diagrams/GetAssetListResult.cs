namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetAssetListResult
{
    public long? Id { get; set; }
    public string? Code { get; set; }
    public string? SerialNumber { get; set; }
    public Supplier? Supplier { get; set; } 
    public Owner? Owner { get; set; } 
    public Warehouse? Warehouse { get; set; }
    public AssetType? AssetType { get; set; } 
    public AssetCategory? AssetCategory { get; set; } 
    public string? Title { get; set; }
    public string? Model { get; set; }
    public string? VersionNumber { get; set; }
    public string? Manufacturer { get; set; }
    public string? ManufactureDate { get; set; }
    public string? OwnershipDate { get; set; }
    public string? InServiceDate { get; set; }
    public string? ExpireDate { get; set; }
    public string? RetiredDate { get; set; }
    public string? Description { get; set; }
    public DataCenter? DataCenter { get; set; }
    public OperationalStatus? OperationalStatus { get; set; } 
    public AssetTechnicalStatus? AssetTechnicalStatus { get; set; }
    public AssetPhysicalStatus? AssetPhysicalStatus { get; set; } 
    public OwnershipType? OwnershipType { get; set; } 
    public string? OwnershipPrepaymentValue { get; set; }
    public string? ownershipPaymentValue { get; set; }
    public UserType? UserType { get; set; } 
    public BusinessCriticality? BusinessCriticality { get; set; }
    public PhysicalLocation? PhysicalLocation { get; set; } 
    public string? HasConfidentialInformation { get; set; }
    public string? ActiveStatus { get; set; }
    public string? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
}

public class GetAssetListResultWrapper
{
    public List<GetAssetListResult>? Data { get; set; }
}

public class Supplier
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class Owner
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? StaffNumber { get; set; }
    public long? CompnayId { get; set; }
    public string? CompanyName { get; set; }
    public long? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public long? BranchId { get; set; }
    public string? BranchName { get; set; }
}

public class Warehouse
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class AssetType
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class AssetCategory
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class DataCenter
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class OperationalStatus
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class AssetTechnicalStatus
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}


public class AssetPhysicalStatus
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class OwnershipType
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class UserType
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class BusinessCriticality
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class PhysicalLocation
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
}