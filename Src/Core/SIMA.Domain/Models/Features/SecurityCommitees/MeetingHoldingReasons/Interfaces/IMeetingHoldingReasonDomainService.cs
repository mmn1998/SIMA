using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Interfaces;

public interface IMeetingHoldingReasonDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string Code, long Id);
}
