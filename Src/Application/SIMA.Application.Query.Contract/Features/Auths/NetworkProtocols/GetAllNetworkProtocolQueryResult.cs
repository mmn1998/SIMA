using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Auths.NetworkProtocols;

public class GetAllNetworkProtocolQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? ActiveStatus { get; set; }
}