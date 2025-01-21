using SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryRequests;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.InquiryRequests
{
    public interface IInquiryRequestQueryRepository : IQueryRepository
    {
        Task<GetInquiryRequestQueryResult> GetById(GetInquiryRequestQuery request);
        Task<Result<IEnumerable<GetInquiryRequestQueryResult>>> GetAll(GetAllInquiryRequestsQuery request);
    }
}
