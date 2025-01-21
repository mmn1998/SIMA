using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Contracts
{
    public interface IBusinessContinuityPlanVersioningRepisitory : IRepository<BusinessContinuityPlanVersioning>
    {
        Task<BusinessContinuityPlanVersioning> GetById(BusinessContinuityPlanVersioningId id);
    }
}
