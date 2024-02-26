using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.State
{
    public class CreateStateCommand : ICommand<Result<long>>
    {
        //public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? WorkFlowId { get; set; }
    }
}
