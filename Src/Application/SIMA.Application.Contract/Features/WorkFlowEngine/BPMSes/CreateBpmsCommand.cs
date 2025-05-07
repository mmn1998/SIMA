using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.BPMSes;

public class CreateBpmsCommand : ICommand<Result<long>>
{
    //public long? MainAggregateId { get; set; }
    public string Data { get; set; }
    //public long ProjectId { get; set; }
    public long WorkFlowId { get; set; }
}
