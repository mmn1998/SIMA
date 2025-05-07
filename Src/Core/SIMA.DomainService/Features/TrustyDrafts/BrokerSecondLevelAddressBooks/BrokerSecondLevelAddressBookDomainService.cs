using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Contracts;
using SIMA.Persistance.Persistence;

namespace SIMA.DomainService.Features.TrustyDrafts.BrokerSecondLevelAddressBooks;

public class BrokerSecondLevelAddressBookDomainService : IBrokerSecondLevelAddressBookDomainService
{
    private readonly SIMADBContext _context;

    public BrokerSecondLevelAddressBookDomainService(SIMADBContext context)
    {
        _context = context;
    }
}