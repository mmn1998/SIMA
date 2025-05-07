using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.BiaValues;

public class ModifyBiaValueCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long ConsequenceIntensionId { get; set; }
    public long ConsequenceId { get; set; }
    public string? Description { get; set; }
}