using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Interfaces;

public interface ISubjectPriorityRepository : IRepository<SubjectPriority>
{
    Task<SubjectPriority> GetById(long Id);
}
