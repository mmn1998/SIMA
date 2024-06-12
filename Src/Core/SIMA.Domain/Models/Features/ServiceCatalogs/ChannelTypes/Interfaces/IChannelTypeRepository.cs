using SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Interfaces;

public interface IChannelTypeRepository : IRepository<ChannelType>
{
    Task<ChannelType> GetById(ChannelTypeId Id);
}
