using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Contracts;

public interface IDataProcedureDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, DataProcedureId? id = null);
}