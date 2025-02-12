using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.ConsequenceIntensions;

public class DeleteConsequenceIntensionCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}