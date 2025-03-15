using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.OrganizationalProjects
{
    public class GetOrganizationalProjectQuery : IQuery<Result<GetOrganizationalProjectsQueryResult>>
    {
        public long Id { get; set; }
    }
}
