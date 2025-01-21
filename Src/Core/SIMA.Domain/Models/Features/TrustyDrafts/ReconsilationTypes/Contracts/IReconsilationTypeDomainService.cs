using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.Contracts;

public interface IReconsilationTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ReconsilationTypeId? id = null);
}