using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Entities;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Contracts;

public interface IBackupPeriodRepository : IRepository<BackupPeriod>
{
    Task<BackupPeriod> GetById(BackupPeriodId id);
}
