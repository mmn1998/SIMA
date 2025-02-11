using SIMA.Domain.Models.Features.BCP.SolutionPeriorities.Entities;
using SIMA.Domain.Models.Features.BCP.SolutionPeriorities.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.SolutionPeriorities.Contracts;

public interface ISolutionPeriorityRepository : IRepository<SolutionPeriority>
{
    Task<SolutionPeriority> GetById(SolutionPeriorityId id);
}