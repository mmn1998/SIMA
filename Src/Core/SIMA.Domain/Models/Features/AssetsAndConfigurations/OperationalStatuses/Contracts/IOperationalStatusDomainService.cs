namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.OperationalStatuses.Contracts;

public interface IOperationalStatusDomainService
{
    Task<bool> IsCodeUnique(string code, OperationalStatusId? id = null);
}