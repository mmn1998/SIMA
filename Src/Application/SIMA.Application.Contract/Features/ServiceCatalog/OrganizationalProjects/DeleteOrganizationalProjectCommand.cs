using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.ServiceCatalog.OrganizationalProjects
{
    public class DeleteOrganizationalProjectCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
