using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Contracts;

public interface IBackupPeriodDomianService : IDomainService
{
    Task<bool> IsCodeUnique(string code, BackupPeriodId? id = null);
}
