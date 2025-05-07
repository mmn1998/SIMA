using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.SysConfigs;

public class GetAllSysConfigQuery : IQuery<Result<List<GetSysConfigQueryResult>>>
{
    public BaseRequest Request { get; set; }
}
