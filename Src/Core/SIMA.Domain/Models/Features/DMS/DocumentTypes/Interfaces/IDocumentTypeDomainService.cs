using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.DMS.DocumentTypes.Interfaces;

public interface IDocumentTypeDomainService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
}
