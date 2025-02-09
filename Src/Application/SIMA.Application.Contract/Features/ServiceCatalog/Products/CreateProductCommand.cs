using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.Products;

public class CreateProductCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? Scope { get; set; }
    public string? Description { get; set; }
    public long ProviderCompanyId { get; set; }
    public long ServiceStatusId { get; set; }
    public string? InServiceDate { get; set; }
    public List<ProductResponsibleCommand>? ProductResponsibles { get; set; }
    public List<CreateChannelList>? Channels { get; set; }


}

public class CreateChannelList
{
    public long ChannelId { get; set; }
}
