using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Locations;

public class GetAllCountriesQuery : IQuery<Result<IEnumerable<GetLocationQueryResult>>>
{
}
