using SIMA.Application.Query.Contract.Features.BranchManagement.BrokerTypes;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Features.BranchManagement.BrokerTypes;

public class BrokerTypeQueryHandler : IQueryHandler<GetAllBrokerTypesQuery, Result<IEnumerable<GetBrokerTypeQueryResult>>>,
IQueryHandler<GetBrokerTypeQuery, Result<GetBrokerTypeQueryResult>>
{
    private readonly IBrokerTypeReadRepository _repository;

    public BrokerTypeQueryHandler(IBrokerTypeReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<GetBrokerTypeQueryResult>>> Handle(GetAllBrokerTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetBrokerTypeQueryResult>> Handle(GetBrokerTypeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        return Result.Ok(result);
    }
}
