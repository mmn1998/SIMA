using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Contracts;

public interface IDataProcedureTypeRepository : IRepository<DataProcedureType>
{
    Task<DataProcedureType> GetById(DataProcedureTypeId id);
}