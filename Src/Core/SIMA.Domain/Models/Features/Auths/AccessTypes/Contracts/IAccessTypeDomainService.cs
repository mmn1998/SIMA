using SIMA.Domain.Models.Features.Auths.AccessTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.AccessTypes.Contracts;

public interface IAccessTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, AccessTypeId? id = null);
}