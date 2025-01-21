using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;

public class GetServiceChannelQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Scope { get; set; }
    public string? Description { get; set; }
    public long? ServiceStatusId { get; set; }
    public string? ServiceStatusName { get; set; }
    public string? ServiceStatusCode { get; set; }
    public DateTime? InServiceDate { get; set; }
    public string? InServiceDatePersian => InServiceDate.ToPersianDate();
}
