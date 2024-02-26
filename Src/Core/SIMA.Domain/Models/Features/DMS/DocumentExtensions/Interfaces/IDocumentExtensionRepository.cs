using SIMA.Domain.Models.Features.DMS.DocumentExtensions.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.DMS.DocumentExtensions.Interfaces;

public interface IDocumentExtensionRepository : IRepository<DocumentExtension>
{
    Task<DocumentExtension> GetById(long id);
}
