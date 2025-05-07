using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Contracts;

public interface IChannelRepository : IRepository<Channel>
{
    Task<Channel> GetById(ChannelId id);
}