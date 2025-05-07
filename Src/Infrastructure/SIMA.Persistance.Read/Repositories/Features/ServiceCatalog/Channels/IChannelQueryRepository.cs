using SIMA.Application.Query.Contract.Features.ServiceCatalog.Channels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.ServiceCatalog.Channels;

public interface IChannelQueryRepository : IQueryRepository
{
    Task<GetChannelQueryResult> GetById(long id);
    Task<GetChannelQueryResult> GetByCode(string code);
    Task<Result<IEnumerable<GetChannelQueryResult>>> GetAll(GetAllChannelsQuery request);
    Task<string> GetLastCode();
}