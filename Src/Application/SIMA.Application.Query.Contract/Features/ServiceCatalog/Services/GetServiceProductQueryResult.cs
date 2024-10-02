using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;

public class GetServiceProductQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Scope { get; set; }
    public string? Description { get; set; }
    public long? ServiceStatusId { get; set; }
    public string? ServiceStatusName { get; set; }
    public string? ServiceStatusCode { get; set; }
    public long? ProviderCompanyId { get; set; }
    public string? ProviderCompanyName { get; set; }
    public string? ProviderCompanyCode { get; set; }
    public DateOnly? InServiceDate { get; set; }
    public string? InServiceDatePersian => InServiceDate.ToPersianDate();
}