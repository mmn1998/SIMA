using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Interfaces;

public interface IIssuePriorityRepository : IRepository<IssuePriority>
{
    Task<IssuePriority> GetById(long id);

}
