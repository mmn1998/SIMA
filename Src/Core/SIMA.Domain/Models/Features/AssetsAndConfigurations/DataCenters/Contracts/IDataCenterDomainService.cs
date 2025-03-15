using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.Contracts;

public interface IDataCenterDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, DataCenterId? id = null);
}