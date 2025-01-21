using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.Channels;

public class CreateChannelCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Scope { get; set; }
    public string? Description { get; set; }
    public long? ServiceStatusId { get; set; }
    public string? InServiceDate { get; set; }
    public List<CreateChannelResponsibleCommand>? ChannelResponsibleList { get; set; }
    public List<CreateProductList>? Products { get; set; }
    public List<CreateServiceList>? Services { get; set; }
    public List<long>? UserTypes { get; set; }
    public List<CreateChannelAccessPointCommand>? ChannelAccessPointList { get; set; }
}
public class CreateChannelResponsibleCommand
{
    public long ResponsibleTypeId { get; set; }
    public long ResponsibleId { get; set; }
}
public class CreateChannelAccessPointCommand
{
    public string? IpAddressTo { get; set; }
    public string? PortTo { get; set; }
    public string? IpAddressFrom { get; set; }
    public string? PortFrom { get; set; }
}

public class CreateProductList
{
    public long ProductId { get; set; }
}

public class CreateServiceList
{
    public long ServiceId { get; set; }
}
