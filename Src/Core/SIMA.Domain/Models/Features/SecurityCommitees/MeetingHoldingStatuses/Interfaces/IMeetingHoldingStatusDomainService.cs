using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Interfaces;

public interface IMeetingHoldingStatusDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string Code, long Id);

}