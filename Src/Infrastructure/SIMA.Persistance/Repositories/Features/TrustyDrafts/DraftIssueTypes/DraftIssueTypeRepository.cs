using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.DraftIssueTypes;

public class DraftIssueTypeRepository : Repository<DraftIssueType>, IDraftIssueTypeRepository
{
    private readonly SIMADBContext _context;

    public DraftIssueTypeRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DraftIssueType> GetById(DraftIssueTypeId Id)
    {
        var entity = await _context.DraftIssueTypes.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}