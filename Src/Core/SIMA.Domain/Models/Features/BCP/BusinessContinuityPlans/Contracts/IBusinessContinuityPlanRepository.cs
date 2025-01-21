using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Contracts
{
    public interface IBusinessContinuityPlanRepository : IRepository<BusinessContinuityPlan>
    {
        Task<BusinessContinuityPlan> GetById(BusinessContinuityPlanId id);
        Task<BusinessContinuityPlanVersioning> GetBusinessContinuityPlanVersioningById(BusinessContinuityPlanId Id);
    }
}
