using SIMA.Application.Query.Contract.Features.Auths.NetworkProtocols;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.Auths.NetworkProtocols;

public interface INetworkProtocolReadRepository: IQueryRepository
{
    Task<Result<IEnumerable<GetAllNetworkProtocolQueryResult>>> GetAll(GetAllNetworlProtocolQuery request);
}