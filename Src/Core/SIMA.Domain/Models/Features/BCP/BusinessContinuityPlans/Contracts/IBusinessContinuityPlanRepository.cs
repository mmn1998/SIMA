using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Contracts;

public interface IBusinessContinuityPlanRepository : IRepository<BusinessContinuityPlan>
{
    Task<BusinessContinuityPlan> GetById(BusinessContinuityPlanId id);
}
