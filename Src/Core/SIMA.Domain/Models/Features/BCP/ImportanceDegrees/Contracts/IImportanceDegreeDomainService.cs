using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Contracts;

public interface IImportanceDegreeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ImportanceDegreeId? id = null);
}