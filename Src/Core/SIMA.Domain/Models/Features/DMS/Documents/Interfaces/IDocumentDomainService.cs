using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.DMS.Documents.Interfaces;

public interface IDocumentDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
    Task<string> GetDocumentExtension(long documentExtensionId);
}
