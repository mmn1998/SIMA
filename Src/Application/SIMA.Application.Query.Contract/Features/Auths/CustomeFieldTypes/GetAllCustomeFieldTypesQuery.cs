using SIMA.Application.Query.Contract.Features.Auths.CustomerTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.CustomeFieldTypes
{
    public class GetAllCustomeFieldTypesQuery : BaseRequest, IQuery<Result<IEnumerable<GetCustomeFieldTypeQueryResult>>>
    {
    }
}
