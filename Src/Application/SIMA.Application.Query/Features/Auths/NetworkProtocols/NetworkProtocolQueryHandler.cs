using SIMA.Application.Query.Contract.Features.Auths.MainAggregates;
using SIMA.Application.Query.Contract.Features.Auths.NetworkProtocols;
using SIMA.Domain.Models.Features.Auths.MainAggregates.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.NetworkProtocols;

namespace SIMA.Application.Query.Features.Auths.NetworkProtocols;

public class NetworkProtocolQueryHandler: IQueryHandler<GetAllNetworlProtocolQuery, Result<IEnumerable<GetAllNetworkProtocolQueryResult>>>
{
    private readonly INetworkProtocolReadRepository _repository;

    public NetworkProtocolQueryHandler(INetworkProtocolReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<GetAllNetworkProtocolQueryResult>>> Handle(GetAllNetworlProtocolQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
