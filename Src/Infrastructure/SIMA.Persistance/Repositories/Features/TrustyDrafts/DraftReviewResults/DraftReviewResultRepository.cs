using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.DraftReviewResults;

public class DraftReviewResultRepository : Repository<DraftReviewResult>, IDraftReviewResultRepository
{
    private readonly SIMADBContext _context;

    public DraftReviewResultRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DraftReviewResult> GetById(DraftReviewResultId Id)
    {
        var entity = await _context.DraftReviewResults.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }

    public async Task<DraftReviewResult> GetByName(string name)
    {
        var entity = await _context.DraftReviewResults.FirstOrDefaultAsync(x => x.Name == name);
        return entity ;
    }
}