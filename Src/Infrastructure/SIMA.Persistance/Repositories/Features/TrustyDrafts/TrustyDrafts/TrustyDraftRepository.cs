using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.TrustyDrafts;

public class TrustyDraftRepository : Repository<TrustyDraft>, ITrustyDraftRepository
{
    private readonly SIMADBContext _context;

    public TrustyDraftRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<TrustyDraft> GetByDarftNumber(string draftNumber)
    {
         var result = await _context.TrustyDrafts
             .Include(x => x.TrustyDraftDocuments)
                 .Include(x => x.TrustyDraftResources)
             .FirstOrDefaultAsync(x => x.DraftNumber == draftNumber && x.ActiveStatusId != 3);
        return result;

    }

    public async Task<TrustyDraft> GetById(TrustyDraftId id)
    {
        try
        {
            var ss = await _context.TrustyDrafts
            //.Include(x => x.TrustyDraftDocuments)
            //.Include(x => x.TrustyDraftResources)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw SimaResultException.NotFound;

            return ss;

        }
        catch(Exception ex)
        {
            throw;
        }
        
    }
}
