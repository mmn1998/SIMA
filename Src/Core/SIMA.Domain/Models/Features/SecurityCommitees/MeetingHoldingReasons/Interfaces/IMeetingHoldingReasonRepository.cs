using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Interfaces;

public interface IMeetingHoldingReasonRepository : IRepository<MeetingHoldingReason>
{
    Task<MeetingHoldingReason> GetById(long Id);
}