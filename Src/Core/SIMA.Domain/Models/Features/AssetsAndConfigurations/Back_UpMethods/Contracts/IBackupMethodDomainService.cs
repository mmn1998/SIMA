using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Back_Up_Methods.Contracts;

public interface IBackupMethodDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, BackupMethodId? id = null);
}