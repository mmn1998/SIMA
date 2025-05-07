using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.SecurityCommitees.MeetingHoldingReasons;

public class MeetingHoldingReasonRepository : Repository<MeetingHoldingReason>, IMeetingHoldingReasonRepository
{
    private readonly SIMADBContext _context;

    public MeetingHoldingReasonRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<MeetingHoldingReason> GetById(long Id)
    {
        var entity = await _context.MeetingHoldingReasons.FirstOrDefaultAsync(x => x.Id == new MeetingHoldingReasonId(Id));
        entity.NullCheck();
        return entity;
    }
}
