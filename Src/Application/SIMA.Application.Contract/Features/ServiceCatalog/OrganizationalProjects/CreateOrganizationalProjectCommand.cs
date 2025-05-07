using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.OrganizationalProjects
{
    public class CreateOrganizationalProjectCommand : ICommand<Result<long>>
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
