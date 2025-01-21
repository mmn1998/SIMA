using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Contracts;

public interface IBrokerSecondLevelAddressBookRepository : IRepository<BrokerSecondLevelAddressBook>
{
    Task<BrokerSecondLevelAddressBook> GetById(BrokerSecondLevelAddressBookId id);
}