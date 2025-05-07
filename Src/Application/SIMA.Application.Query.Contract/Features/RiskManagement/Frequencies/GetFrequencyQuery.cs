using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.Frequencies;

public class GetFrequencyQuery: IQuery<Result<GetFrequencyQueryResult>>
{
    public long Id { get; set; }
}