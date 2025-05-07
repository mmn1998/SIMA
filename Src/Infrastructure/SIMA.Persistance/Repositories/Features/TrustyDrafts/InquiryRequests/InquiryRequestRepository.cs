using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.TrustyDrafts.InquiryRequests
{
    public class InquiryRequestRepository : Repository<InquiryRequest>, IInquiryRequestRepository
    {
        private readonly SIMADBContext _context;

        public InquiryRequestRepository(SIMADBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<InquiryRequest> GetById(InquiryRequestId Id)
        {
            var entity = await _context.InquiryRequests
                .Include(x => x.InquiryResponses)
                .Include(x => x.InquiryRequestCurrencies)
                .Include(x => x.InquiryRequestDocuments)
                .FirstOrDefaultAsync(x => x.Id == Id);
            entity.NullCheck();
            return entity ?? throw SimaResultException.NotFound;
        }
    }
}
