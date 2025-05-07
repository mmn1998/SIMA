using SIMA.Domain.Models.Features.BCP.Origins.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.Origins.Contracts;

public interface IOriginDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, OriginId? id = null);
}