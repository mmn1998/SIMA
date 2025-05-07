using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.BrokerSecondLevelAddressBooks;

public class BrokerSecondLevelAddressBookRepository : Repository<BrokerSecondLevelAddressBook>, IBrokerSecondLevelAddressBookRepository
{
    private readonly SIMADBContext _context;

    public BrokerSecondLevelAddressBookRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<BrokerSecondLevelAddressBook> GetById(BrokerSecondLevelAddressBookId Id)
    {
        var entity = await _context.BrokerSecondLevelAddressBooks.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}