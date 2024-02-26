using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.BranchManagement.BrokerTypes
{
    public class CreateBrokerTypeCommand : ICommand<Result<long>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
