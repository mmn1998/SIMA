using SIMA.Domain.Models.Features.DMS.DocumentTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.DMS.DocumentTypes.Interfaces;

public interface IDocumentTypeRepository : IRepository<DocumentType>
{
    Task<DocumentType> GetById(long id);
}
