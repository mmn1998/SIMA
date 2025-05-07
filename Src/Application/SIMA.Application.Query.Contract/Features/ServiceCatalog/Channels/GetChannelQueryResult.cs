using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Channels;

public class GetChannelQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Scope { get; set; }
    public string? Description { get; set; }
    public long? ServiceStatusId { get; set; }
    public string? ServiceStatusName { get; set; }
    public DateTime? InServiceDate { get; set; }
    public string? InServiceDatePersian => InServiceDate.ToPersianDate();
    public string? ActiveStatus { get; set; }
    public IEnumerable<GetChannelResponsibleQuery>? ChannelResponsibleList { get; set; }
    public IEnumerable<GetProductChannelQuery>? ProductChannelList { get; set; }
    public IEnumerable<GetChannelUserTypeQuery>? ChannelUserTypeList { get; set; }
    public IEnumerable<GetChannelAccessPointQuery>? ChannelAccessPointList { get; set; }
    public IEnumerable<GetServiceChannelQuery>? ServiceChannelList { get; set; }
}
public class GetChannelResponsibleQuery
{
    public long ResponsibleTypeId { get; set; }
    public string? ResponsibleTypeName { get; set; }
    public long ResponsibleId { get; set; }
    public string? Responsible { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public long? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public long? BranchId { get; set; }
    public string? BranchName { get; set; }
}
public class GetProductChannelQuery
{
    public long ProductId { get; set; }
    public string? ProductName { get; set; }
}
public class GetChannelUserTypeQuery
{
    public long UserTypeId { get; set; }
    public string? UserTypeName { get; set; }
}
public class GetChannelAccessPointQuery
{
    public string? IpAddressTo { get; set; }
    public string? PortTo { get; set; }
    public string? IpAddressFrom { get; set; }
    public string? PortFrom { get; set; }
}
public class GetServiceChannelQuery
{
    public long? ServiceId { get; set; }
    public string? ServiceName { get; set; }
}