using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.OperationalStatuses.Contracts;

public interface IOperationalStatusDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, OperationalStatusId? id = null);
}