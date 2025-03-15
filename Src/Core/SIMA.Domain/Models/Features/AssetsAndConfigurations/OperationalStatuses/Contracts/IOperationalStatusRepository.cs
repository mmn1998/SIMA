using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.OperationalStatuses.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.OperationalStatuses.Contracts;

public interface IOperationalStatusRepository: IRepository<OperationalStatus>
{
    Task<OperationalStatus> GetById(OperationalStatusId id);
}