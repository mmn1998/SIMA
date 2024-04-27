using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Interfaces;

public interface IMeetingRepository : IRepository<Meeting>
{
    Task<Meeting> GetById(long Id);
}
