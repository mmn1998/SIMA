using SIMA.Domain.Models.Features.BCP.SolutionPriorities.Entities;
using SIMA.Domain.Models.Features.BCP.SolutionPriorities.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.SolutionPriorities.Contracts;

public interface ISolutionPriorityRepository : IRepository<SolutionPriority>
{
    Task<SolutionPriority> GetById(SolutionPriorityId id);
}