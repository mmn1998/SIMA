using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BCP.BiaValues.Contracts;
using SIMA.Domain.Models.Features.BCP.BiaValues.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BCP.BiaValues;

public class BiaValueRepository : Repository<BiaValue>, IBiaValueRepository
{
    private readonly SIMADBContext _context;

    public BiaValueRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<BiaValue> GetById(BiaValueId Id)
    {
        var entity = await _context.BiaValues.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}