using SIMA.Application.Query.Contract.Features.Auths.MainAggregates;
using SIMA.Domain.Models.Features.Auths.MainAggregates.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Features.Auths.MainAggregates;

public class MainAggregateQueryHandler : IQueryHandler<GetAllMainAggregateQuery, Result<List<GetMainAggregateQueryResult>>>
{
    private readonly IMainAggregateReadRepository _repository;

    public MainAggregateQueryHandler(IMainAggregateReadRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<List<GetMainAggregateQueryResult>>> Handle(GetAllMainAggregateQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}
