using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.Channels;

public class ModifyChannelCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
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
