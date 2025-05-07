using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.SupplierRanks.Contracts;
using SIMA.Domain.Models.Features.Auths.SupplierRanks.Entities;
using SIMA.Domain.Models.Features.Auths.SupplierRanks.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.Auths.SupplierRanks;

public class SupplierRankRepository : Repository<SupplierRank>, ISupplierRankRepository
{
    private readonly SIMADBContext _context;

    public SupplierRankRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SupplierRank> GetById(SupplierRankId Id)
    {
        var entity = await _context.SupplierRanks.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}