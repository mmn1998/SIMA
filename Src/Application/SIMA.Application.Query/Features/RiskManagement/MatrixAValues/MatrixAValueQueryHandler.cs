using SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAs;
using SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAValues;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;


namespace SIMA.Application.Query.Features.RiskManagement.MatrixAValues;

public class MatrixAValueQueryHandler: IQueryHandler<GetMatrixAValueQuery, Result<GetMatrixAValueQueryResult>>,
    IQueryHandler<GetAllMatrixAValuesQuery, Result<IEnumerable<GetMatrixAValueQueryResult>>>
{
    public Task<Result<GetMatrixAValueQueryResult>> Handle(GetMatrixAValueQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<GetMatrixAValueQueryResult>>> Handle(GetAllMatrixAValuesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}