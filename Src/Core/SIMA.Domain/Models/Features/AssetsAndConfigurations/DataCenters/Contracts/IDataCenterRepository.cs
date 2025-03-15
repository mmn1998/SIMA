using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.Contracts;

public interface IDataCenterRepository : IRepository<DataCenter>
{
    Task<DataCenter> GetById(DataCenterId id);
}