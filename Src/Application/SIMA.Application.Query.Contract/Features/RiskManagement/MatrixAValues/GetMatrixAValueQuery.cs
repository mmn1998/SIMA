using SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAs;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAValues;

public class GetMatrixAValueQuery: IQuery<Result<GetMatrixAValueQueryResult>>
{
    public long Id { get; set; }
}