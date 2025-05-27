using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.ConsequenceIntensionDescriptions;

public class ModifyConsequenceIntensionDescriptionCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long ConsequenceIntensionId { get; set; }
    public long ConsequenceId { get; set; }
    public string? Description { get; set; }
}