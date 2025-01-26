using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.SeverityValues;

public class GetSeverityValueQuery : IQuery<Result<GetSeverityValueQueryResult>>
{
    public long Id { get; set; }
}