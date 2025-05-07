using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.ServiceCatalog.ServicePriorities
{
    public class CreateServicePriorityCommand : ICommand<Result<long>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Ordering { get; set; }
    }
}
