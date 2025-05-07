using SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryResponses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.InquiryResponses
{
    public interface IInquiryResponseQueryRepository : IQueryRepository
    {
        Task<GetInquiryResponseQueryResult> GetById(GetInquiryResponseQuery request);
        Task<Result<IEnumerable<GetInquiryResponseQueryResult>>> GetAll(GetAllInquiryResponsesQuery request);
        Task<Result<IEnumerable<GetInquiryResponseQueryResult>>> GetAllAcceptResponse();
    }
}
