using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.InquiryResponses;

public class InquiryResponseRepository : Repository<InquiryResponse>, IInquiryResponseRepository
{
    private readonly SIMADBContext _context;

    public InquiryResponseRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<InquiryResponse> GetById(InquiryResponseId Id)
    {
        var entity = await _context.InquiryResponses.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }

    public async Task<InquiryResponse> GetByInqueryRequestId(InquiryRequestId inquiryRequestId)
    {
        var entity = await _context.InquiryResponses.FirstOrDefaultAsync(x => x.InquiryRequestId == inquiryRequestId);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }
}
