using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.Contracts;

public interface IRequestValorDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, RequestValorId? id = null);
}