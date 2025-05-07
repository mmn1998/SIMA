using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.ApiTypes;

public class GetApiTypeQuery : IQuery<Result<GetApiTypesQueryResult>>
{
    public long Id { get; set; }
}