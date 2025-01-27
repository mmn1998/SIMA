using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Severities;

public class GetSeverityQuery : IQuery<Result<GetSeverityQueryResult>>
{
    public long Id { get; set; }
}