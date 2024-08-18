using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Contracts
{
    public interface IBusinessContinuityPlanStratgyRepisitory : IRepository<BusinessContinuityPlanStratgy>
    {
        Task<BusinessContinuityPlanStratgy> GetById(BusinessContinuityPlanStratgyId id);
    }
}
