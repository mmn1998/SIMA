using SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.Interfaces;

public interface IMeetingScheduleRepository : IRepository<MeetingSchedule>
{
    Task<MeetingSchedule> GetById(long id);
}
