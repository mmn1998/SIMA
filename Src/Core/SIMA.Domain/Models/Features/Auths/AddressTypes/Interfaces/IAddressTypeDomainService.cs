using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.AddressTypes.Interfaces;

public interface IAddressTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
