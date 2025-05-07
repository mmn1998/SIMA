using SIMA.Application.Query.Contract.Features.TrustyDrafts.BrokerSecondLevelAddressBooks;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.BrokerSecondLevelAddressBooks;

namespace SIMA.Application.Query.Features.TrustyDrafts.BrokerSecondLevelAddressBooks;

public class BrokerSecondLevelAddressBookQueryHandler : IQueryHandler<GetBrokerSecondLevelAddressBookQuery, Result<GetBrokerSecondLevelAddressBookQueryResult>>,
    IQueryHandler<GetAllBrokerSecondLevelAddressBooksQuery, Result<IEnumerable<GetBrokerSecondLevelAddressBookQueryResult>>>
{
    private readonly IBrokerSecondLevelAddressBookQueryRepository _repository;

    public BrokerSecondLevelAddressBookQueryHandler(IBrokerSecondLevelAddressBookQueryRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<GetBrokerSecondLevelAddressBookQueryResult>> Handle(GetBrokerSecondLevelAddressBookQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request);
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetBrokerSecondLevelAddressBookQueryResult>>> Handle(GetAllBrokerSecondLevelAddressBooksQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }
}