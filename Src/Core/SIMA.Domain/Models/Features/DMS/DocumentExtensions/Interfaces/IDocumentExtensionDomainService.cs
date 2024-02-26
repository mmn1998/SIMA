using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.DMS.DocumentExtensions.Interfaces;

public interface IDocumentExtensionDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
