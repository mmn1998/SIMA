using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Entities;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Interfaces;
using SIMA.Domain.Models.Features.Auths.AddressTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class AddressTypeRepository : Repository<AddressType>, IAddressTypeRepository
{
    private readonly SIMADBContext _context;

    public AddressTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<AddressType> GetById(long id)
    {
        var entity = await _context.AddressTypes.FirstOrDefaultAsync(a => a.Id == new AddressTypeId(id));
        entity.NullCheck();
        return entity;
    }
}
