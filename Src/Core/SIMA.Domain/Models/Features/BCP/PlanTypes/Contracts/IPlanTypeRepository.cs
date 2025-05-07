using SIMA.Domain.Models.Features.BCP.PlanTypes.Entities;
using SIMA.Domain.Models.Features.BCP.PlanTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.PlanTypes.Contracts;

public interface IPlanTypeRepository : IRepository<PlanType>
{
    Task<PlanType> GetById(PlanTypeId id);
}
