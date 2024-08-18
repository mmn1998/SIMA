using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Contracts
{
    public interface IPlanResponsibilityRepository : IRepository<PlanResponsibility>
    {
        Task<PlanResponsibility> GetById(PlanResponsibilityId id);
    }
}
