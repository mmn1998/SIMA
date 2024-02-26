using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Companies;

public class GetAllCompanyQuery : IQuery<Result<List<GetCompanyQueryResult>>>
{
    public BaseRequest Request { get; set; }
}
