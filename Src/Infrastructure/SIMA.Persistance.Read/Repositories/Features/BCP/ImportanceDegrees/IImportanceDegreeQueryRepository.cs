using SIMA.Application.Query.Contract.Features.BCP.ImportanceDegrees;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.BCP.ImportanceDegrees;

public interface IImportanceDegreeQueryRepository : IQueryRepository
{
    Task<GetImportanceDegreeQueryResult> GetById(GetImportanceDegreeQuery request);
    Task<Result<IEnumerable<GetImportanceDegreeQueryResult>>> GetAll(GetAllImportanceDegreesQuery request);
}