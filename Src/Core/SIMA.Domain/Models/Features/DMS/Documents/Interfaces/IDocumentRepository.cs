using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.DMS.Documents.Interfaces;

public interface IDocumentRepository : IRepository<Document>
{
    Task<Document> GetById(long id);
}
