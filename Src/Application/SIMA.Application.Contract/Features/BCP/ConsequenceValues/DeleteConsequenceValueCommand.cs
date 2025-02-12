using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.ConsequenceValues;

public class DeleteConsequenceValueCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}