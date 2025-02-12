using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.BiaValues;

public class GetBiaValueQuery : IQuery<Result<GetBiaValueQueryResult>>
{
    public long Id { get; set; }
}