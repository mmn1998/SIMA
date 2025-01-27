using SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAs;
using SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.RiskManagement.MatrixAValues;

public interface IMatrixAValueQueryRepository: IQueryRepository
{
    Task<GetMatrixAValueQueryResult> GetById(GetMatrixAValueQuery request);
    Task<Result<IEnumerable<GetMatrixAValueQueryResult>>> GetAll(GetAllMatrixAValuesQuery request);
}