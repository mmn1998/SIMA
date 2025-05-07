using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCategories;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.OrganizationalProjects
{
    public class GetAllOrganizationalProjectsQuery : BaseRequest, IQuery<Result<IEnumerable<GetOrganizationalProjectsQueryResult>>>
    {
    }
}
