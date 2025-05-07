using SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAs;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.MatrixAs;

public interface IMatrixAQueryRepository : IQueryRepository
{
    Task<GetMatrixAQueryResult> GetById(GetMatrixAQuery request);
    Task<Result<IEnumerable<GetMatrixAQueryResult>>> GetAll(GetAllMatrixAsQuery request);
}