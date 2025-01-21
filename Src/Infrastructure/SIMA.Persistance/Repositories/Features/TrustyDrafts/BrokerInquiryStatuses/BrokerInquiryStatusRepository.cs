using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Contarcts;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.BrokerInquiryStatuses;

public class BrokerInquiryStatusRepository : Repository<BrokerInquiryStatus>, IBrokerInquiryStatusRepository
{
    private readonly SIMADBContext _context;

    public BrokerInquiryStatusRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<BrokerInquiryStatus> GetById(BrokerInquiryStatusId Id)
    {
        var entity = await _context.BrokerInquiryStatuses.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}