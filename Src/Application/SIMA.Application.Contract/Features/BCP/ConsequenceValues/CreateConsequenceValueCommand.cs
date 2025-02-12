using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.ConsequenceValues;

public class CreateConsequenceValueCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public long OriginId { get; set; }
    public long ConsequenceId { get; set; }
    public float ValueNumber { get; set; }
}