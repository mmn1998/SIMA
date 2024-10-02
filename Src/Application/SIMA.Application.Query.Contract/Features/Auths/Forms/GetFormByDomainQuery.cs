using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Forms
{
    public class GetFormByDomainQuery : BaseRequest, IQuery<Result<IEnumerable<GetFormQueryResult>>>
    {
        public long DomainId { get; set; }
    }
}
