using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.ConsequenceValues;

public class ModifyConsequenceValueCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long OriginId { get; set; }
    public long ConsequenceId { get; set; }
    public float ValueNumber { get; set; }
}