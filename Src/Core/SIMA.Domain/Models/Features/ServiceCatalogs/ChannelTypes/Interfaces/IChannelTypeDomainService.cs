using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Interfaces;

public interface IChannelTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ChannelTypeId? Id = null);
}
