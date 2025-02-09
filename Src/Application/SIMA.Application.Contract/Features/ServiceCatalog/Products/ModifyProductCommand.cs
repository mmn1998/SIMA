using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.ServiceCatalog.Products;

public class ModifyProductCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Scope { get; set; }
    public string? Description { get; set; }
    public long ProviderCompanyId { get; set; }
    public long ServiceStatusId { get; set; }
    public string? InServiceDate { get; set; }
    public List<ProductResponsibleCommand>? ProductResponsibles { get; set; }
    public List<ProductChannelCommand>? Channels { get; set; }
}
