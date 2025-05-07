using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Contracts
{
    public interface IBusinessContinuityStratgySolutionRepisitory : IRepository<BusinessContinuityStratgySolution>
    {
        Task<BusinessContinuityStratgySolution> GetById(BusinessContinuityStratgySolutionId id);
    }
}
