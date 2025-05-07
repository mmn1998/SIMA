using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.MatrixAs;

public class GetMatrixAQuery : IQuery<Result<GetMatrixAQueryResult>>
{
    public long Id { get; set; }
}