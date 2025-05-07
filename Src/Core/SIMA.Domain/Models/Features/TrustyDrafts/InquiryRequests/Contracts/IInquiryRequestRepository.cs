using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Contracts;

public interface IInquiryRequestRepository : IRepository<InquiryRequest>
{
    Task<InquiryRequest> GetById(InquiryRequestId id);
}
