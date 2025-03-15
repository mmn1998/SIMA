using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Contracts;

public interface IDataProcedureRepository : IRepository<DataProcedure>
{
    Task<DataProcedure> GetById(DataProcedureId id);
}