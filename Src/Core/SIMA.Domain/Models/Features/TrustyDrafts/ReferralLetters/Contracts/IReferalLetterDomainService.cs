using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Contracts;

public interface IReferalLetterDomainService : IDomainService
{
    Task<string?> GetLastLetterNumber();
    Task<string?> GetBrokerTypeCodeByBrokerId(BrokerId brokerId);
}
