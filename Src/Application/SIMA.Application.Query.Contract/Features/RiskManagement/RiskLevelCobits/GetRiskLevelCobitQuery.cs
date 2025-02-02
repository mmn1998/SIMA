using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelCobits;

public class GetRiskLevelCobitQuery : IQuery<Result<GetRiskLevelCobitQueryResult>>
{
    public long Id { get; set; }
}