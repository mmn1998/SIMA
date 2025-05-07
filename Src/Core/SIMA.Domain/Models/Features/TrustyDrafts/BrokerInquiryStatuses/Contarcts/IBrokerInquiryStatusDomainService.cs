using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Contarcts;

public interface IBrokerInquiryStatusDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, BrokerInquiryStatusId? id = null);
}
