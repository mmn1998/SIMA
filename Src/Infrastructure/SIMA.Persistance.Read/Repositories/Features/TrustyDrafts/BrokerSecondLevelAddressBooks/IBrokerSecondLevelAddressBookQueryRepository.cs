using SIMA.Application.Query.Contract.Features.TrustyDrafts.BrokerSecondLevelAddressBooks;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.BrokerSecondLevelAddressBooks;

public interface IBrokerSecondLevelAddressBookQueryRepository : IQueryRepository
{
    Task<GetBrokerSecondLevelAddressBookQueryResult> GetById(GetBrokerSecondLevelAddressBookQuery request);
    Task<Result<IEnumerable<GetBrokerSecondLevelAddressBookQueryResult>>> GetAll(GetAllBrokerSecondLevelAddressBooksQuery request);
}