using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.BusinessEntities.Contracts
{
    public interface IBusinessEntityDomainService : IDomainService
    {
        Task<bool> IsCodeUnique(string code, long id);
    }
}
