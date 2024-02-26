using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.BranchManagement.BrokerTypes
{
    public class DeleteBrokerTypeCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
