using SIMA.Application.Query.Contract.Features.TrustyDrafts.BrokerInquiryStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.TrustyDrafts.BrokerInquiryStatuses;

public interface IBrokerInquiryStatusQueryRepository : IQueryRepository
{
    Task<GetBrokerInquiryStatusQueryResult> GetById(GetBrokerInquiryStatusQuery request);
    Task<Result<IEnumerable<GetBrokerInquiryStatusQueryResult>>> GetAll(GetAllBrokerInquiryStatusesQuery request);
}