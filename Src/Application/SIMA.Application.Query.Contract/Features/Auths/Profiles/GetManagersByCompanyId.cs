using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Profiles;

public class GetManagersByCompanyId : IQuery<Result<List<SelectModel>>>
{
    public long Id { get; set; }
}
