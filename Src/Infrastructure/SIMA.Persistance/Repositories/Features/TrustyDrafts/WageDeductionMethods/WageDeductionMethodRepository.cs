using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.WageDeductionMethods.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.WageDeductionMethods.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.WageDeductionMethods.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.WageDeductionMethods;

public class WageDeductionMethodRepository : Repository<WageDeductionMethod>, IWageDeductionMethodRepository
{
    private readonly SIMADBContext _context;

    public WageDeductionMethodRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<WageDeductionMethod> GetById(WageDeductionMethodId Id)
    {
        var entity = await _context.WageDeductionMethods.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}