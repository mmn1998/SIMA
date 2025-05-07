using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.ReconsilationTypes;

public class ReconsilationTypeRepository : Repository<ReconsilationType>, IReconsilationTypeRepository
{
    private readonly SIMADBContext _context;

    public ReconsilationTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ReconsilationType> GetById(ReconsilationTypeId Id)
    {
        var entity = await _context.ReconsilationTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}