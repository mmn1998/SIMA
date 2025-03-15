using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Contracts;

public interface IDataProcedureTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, DataProcedureTypeId? id = null);
}