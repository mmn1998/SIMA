using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Contarcts;

public interface IBrokerInquiryStatusRepository : IRepository<BrokerInquiryStatus>
{
    Task<BrokerInquiryStatus> GetById(BrokerInquiryStatusId id);
}
