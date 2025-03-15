using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMA.Application.Contract.Features.ServiceCatalog.OrganizationalProjects
{
    public class ModifyOrganizationalProjectCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
