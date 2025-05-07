using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Contracts
{
    public interface IBusinessContinuityStratgyResponsibleRepisitory : IRepository<BusinessContinuityStratgyResponsible>
    {
        Task<BusinessContinuityStratgyResponsible> GetById(BusinessContinuityStratgyResponsibleId id);
    }
}
