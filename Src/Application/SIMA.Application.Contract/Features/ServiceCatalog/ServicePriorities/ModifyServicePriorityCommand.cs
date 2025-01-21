using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.ServiceCatalog.ServicePriorities
{
    public class ModifyServicePriorityCommand :  ICommand<Result<long>>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Ordering { get; set; }
    }
}
