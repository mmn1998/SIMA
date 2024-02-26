using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.Companies.Interfaces;

public interface ICompanyService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
    Task<bool> IsCompanyParent(long? companyId);
}
