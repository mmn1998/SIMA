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
    public DateOnly? InServiceDate { get; set; }
    public string? InServiceDatePersian => InServiceDate.ToPersianDate();
    public string? ActiveStatus { get; set; }
    public IEnumerable<GetChannelResponsibleQuery>? ChannelResponsibleList { get; set; }
    public IEnumerable<GetProductChannelQuery>? ProductChannelList { get; set; }
    public IEnumerable<GetChannelUserTypeQuery>? ChannelUserTypeList { get; set; }
    public IEnumerable<GetChannelAccessPointQuery>? ChannelAccessPointList { get; set; }
}
public class GetChannelResponsibleQuery
{
    public long ResponsibleTypeId { get; set; }
    public string? ResponsibleTypeName { get; set; }
    public long ResponsibleId { get; set; }
    public string? Responsible { get; set; }
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
    public string? IpAddress { get; set; }
    public string? Port { get; set; }
}