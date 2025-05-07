using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.BPMSes;

public class ModifyBpmsCommand : ICommand<Result<long>>
{
    public string Data { get; set; }
    public long WorkFlowId { get; set; }
}
