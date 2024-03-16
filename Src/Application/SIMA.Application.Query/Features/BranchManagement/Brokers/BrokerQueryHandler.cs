using SIMA.Application.Query.Contract.Features.BranchManagement.Brokers;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Features.BranchManagement.Brokers;

public class BrokerQueryHandler : IQueryHandler<GetBrokerQuery, Result<GetBrokerQueryResult>>, IQueryHandler<GetAllBrokerQuery, Result<IEnumerable<GetBrokerQueryResult>>>
{
    private readonly IBrokerReadRepository _repository;

    public BrokerQueryHandler(IBrokerReadRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<GetBrokerQueryResult>>> Handle(GetAllBrokerQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetBrokerQueryResult>> Handle(GetBrokerQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}
