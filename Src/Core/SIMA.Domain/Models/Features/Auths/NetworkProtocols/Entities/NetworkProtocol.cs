using SIMA.Domain.Models.Features.Auths.NetworkProtocols.Args;
using SIMA.Domain.Models.Features.Auths.NetworkProtocols.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Auths.NetworkProtocols.Entities;

public class NetworkProtocol : Entity, IAggregateRoot
{
    private NetworkProtocol() { }
    private NetworkProtocol(CreateNetworkProtocolArg arg)
    {

    }
    public NetworkProtocolId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<Api> _apis = new();
    public ICollection<Api> Apis => _apis;
}
