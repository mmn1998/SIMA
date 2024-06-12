using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Entities;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Contracts;

public interface IImportanceDegreeRepository : IRepository<ImportanceDegree>
{
    Task<ImportanceDegree> GetById(ImportanceDegreeId id);
}