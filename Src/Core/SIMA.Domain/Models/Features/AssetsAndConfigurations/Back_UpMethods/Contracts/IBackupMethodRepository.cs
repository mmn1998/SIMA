using SIMA.Domain.Models.Features.AssetsAndConfigurations.Back_Up_Methods.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Back_Up_Methods.Contracts;

public interface IBackupMethodRepository : IRepository<BackupMethod>
{
    Task<BackupMethod> GetById(BackupMethodId id);
}