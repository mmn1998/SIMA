using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.Auths.ViewLists.ViewField
{
    public class GetViewFieldQuery : IQuery<Result<GetViewFieldQueryResult>>
    {
        public long Id { get; set; }
    }
}
