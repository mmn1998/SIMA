using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Contracts;

public interface IChannelDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ChannelId? id = null);
    Task<string?> GetLastCode();
}