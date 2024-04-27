using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.Forms
{
    public class GetFormQuery : IQuery<Result<GetFormQueryResult>>
    {
        public long Id { get; set; }
    }
}
