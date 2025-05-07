using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.Contrcts;

public interface ICancellationResaonDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, CancellationResaonId? id = null);
}