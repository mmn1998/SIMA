using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.BCP.ImportanceDegrees;

public class GetImportanceDegreeQuery : IQuery<Result<GetImportanceDegreeQueryResult>>
{
    public long Id { get; set; }
}