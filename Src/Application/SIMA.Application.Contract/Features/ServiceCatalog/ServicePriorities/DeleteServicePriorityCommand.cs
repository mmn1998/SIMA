using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.ServiceCatalog.ServicePriorities
{
    public class DeleteServicePriorityCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }

    }
}
