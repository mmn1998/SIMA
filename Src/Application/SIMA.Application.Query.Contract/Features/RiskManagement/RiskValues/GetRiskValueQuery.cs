using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskValues;

public class GetRiskValueQuery : IQuery<Result<GetRiskValueQueryResult>>
{
    public long Id { get; set; }
}