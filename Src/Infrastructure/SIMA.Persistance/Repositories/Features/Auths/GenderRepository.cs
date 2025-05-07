using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.Genders;
using SIMA.Domain.Models.Features.Auths.Genders.Entities;
using SIMA.Domain.Models.Features.Auths.Genders.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths;

public class GenderRepository : Repository<Gender>, IGenderRepository
{
    private readonly SIMADBContext _context;

    public GenderRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Gender> GetById(long id)
    {
        var entity = await _context.Genders.FirstOrDefaultAsync(x => x.Id == new GenderId(id));
        entity.NullCheck();
        return entity;
    }
}
