using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Contracts
{
    public interface IBusinessContinuityStratgyResponsibleDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, BusinessContinuityStratgyResponsibleId? id = null);
    }
}
