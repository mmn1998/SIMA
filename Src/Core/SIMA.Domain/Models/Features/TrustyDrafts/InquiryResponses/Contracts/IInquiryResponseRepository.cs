using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.Responses.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Contracts
{
    public interface  IInquiryResponseRepository : IRepository<InquiryResponse>
    {
        Task<InquiryResponse> GetById(InquiryResponseId id);
        Task<InquiryResponse> GetByInqueryRequestId(InquiryRequestId inquiryRequestId);
    }
}
