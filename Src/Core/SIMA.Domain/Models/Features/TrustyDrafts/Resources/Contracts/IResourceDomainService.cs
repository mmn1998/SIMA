using SIMA.Domain.Models.Features.TrustyDrafts.Resources.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.TrustyDrafts.Resources.Contracts;

public interface IResourceDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ResourceId? id = null);
}