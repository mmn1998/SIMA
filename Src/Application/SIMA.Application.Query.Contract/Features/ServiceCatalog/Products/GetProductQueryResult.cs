using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Products;

public class GetProductQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Scope { get; set; }
    public string? Description { get; set; }
    public long? ProviderCompanyId { get; set; }
    public string? ProviderCompanyName { get; set; }
    public long ServiceStatusId { get; set; }
    public string? ServiceStatusName { get; set; }
    public DateTime? InServiceDate { get; set; }
    public string? InServiceDatePersian => InServiceDate.ToPersianDate();
    public List<ProductResponsibleQuery>? ProductResponsibles { get; set; }
    public List<ChannelQuery>? ProductChannels { get; set; }
}
