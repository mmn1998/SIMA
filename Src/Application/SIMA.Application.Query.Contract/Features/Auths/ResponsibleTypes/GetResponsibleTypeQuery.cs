using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.ResponsibleTypes;

public class GetResponsibleTypeQuery : IQuery<Result<GetResponsibleTypeQueryResult>>
{
    public long Id { get; set; }
}