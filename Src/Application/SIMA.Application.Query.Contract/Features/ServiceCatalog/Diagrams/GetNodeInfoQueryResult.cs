namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetNodeInfoQueryResult
{
    
}




public class GetNodeInfoQueryResultProductWrapper
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Scope { get; set; }
    public string? Description { get; set; }
    public string? ProviderCompanyId { get; set; }
    public string? ProviderCompanyName { get; set; }
    public string? ServiceStatusId { get; set; }
    public string? InServiceDate { get; set; }
    public string? ServiceStatusName { get; set; }
    public List<ProductResponsibles> ProductResponsibles { get; set; }
    public List<ProductChannels> ProductChannels { get; set; }
    public string? ActiveStatus { get; set; }
}

public class GetNodeInfoQueryResultChannelWrapper
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Scope { get; set; }
    public string? Description { get; set; }
    public string? ServiceStatusId { get; set; }
    public string? InServiceDate { get; set; }
    public List<ChannelResponsibles> ChannelResponsibles { get; set; }
    public List<Products> Products { get; set; }
    public List<Services> Services { get; set; }
    public List<UserTypes> UserTypes { get; set; }
    public List<ChannelAccessPoint> ChannelAccessPoint { get; set; }
    public string? ActiveStatus { get; set; }
}

public class GetNodeInfoQueryResultServiceWrapper
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }
    public List<ServiceType> ServiceType { get; set; }
    public List<ServiceCategory> ServiceCategory { get; set; }
    public List<TechnicalSupervisorDepartment> TechnicalSupervisorDepartment { get; set; }
    public string? isCriticalService { get; set; }
    public string? Description { get; set; }
    public string? ServiceStatusId { get; set; }
    public string? ServiceStatusName { get; set; }
    public string? ServiceCategoryId { get; set; }
    public string? ServiceCategoryName { get; set; }
    public string? inServiceDate { get; set; }
    public string? ServiceCost { get; set; }
    public string? WorkflowFileContent { get; set; }
    public string? FeedbackUrl { get; set; }
    public List<CustomerTypes> CustomerTypes { get; set; }
    public List<ChannelAccessPoint> ChannelAccessPoint { get; set; }
    public string? ActiveStatus { get; set; }
}


public class GetNodeInfoQueryResultWrapper
{
    public List<GetNodeInfoQueryResult>? data { get; set; }
}
