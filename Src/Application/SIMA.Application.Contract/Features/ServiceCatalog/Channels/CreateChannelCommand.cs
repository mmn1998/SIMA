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
    public List<long>? Products { get; set; }
    public List<long>? Services { get; set; }
    public List<long>? UserTypes { get; set; }
    public List<CreateChannelAccessPointCommand>? ChannelAccessPointList { get; set; }
}
public class CreateChannelResponsibleCommand
{
    public long ChannelResponsibleTypeId { get; set; }
    public long ResponsibleId { get; set; }
}
public class CreateChannelAccessPointCommand
{
    public string? IpAddress { get; set; }
    public string? Port { get; set; }
}
