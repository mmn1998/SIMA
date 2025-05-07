using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Interfaces;

public interface IMeetingHoldingStatusRepository : IRepository<MeetingHoldingStatus>
{
    Task<MeetingHoldingStatus> GetById(long Id);
}
