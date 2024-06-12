using SIMA.Application.Query.Contract.Features.ServiceCatalog.ChannelTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.ChannelTypes;

public interface IChannelTypeQueryRepository : IQueryRepository
{
    Task<GetChannelTypeQueryResult> GetById(GetChannelTypeQuery request);
    Task<Result<IEnumerable<GetChannelTypeQueryResult>>> GetAll(GetAllChannelTypeQuery request);
}