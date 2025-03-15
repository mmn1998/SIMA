using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Contracts;

public interface IApiDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, ApiId? id = null);
}
