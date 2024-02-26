using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.Entities;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.Repositories;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class PhoneTypeRepository : Repository<PhoneType>, IPhoneTypeRepository
{
    private readonly SIMADBContext _context;

    public PhoneTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PhoneType> GetById(long id)
    {
        var entity = await _context.PhoneTypes.FirstOrDefaultAsync(x => x.Id == new PhoneTypeId(id));
        entity.NullCheck();
        return entity;
    }
}
