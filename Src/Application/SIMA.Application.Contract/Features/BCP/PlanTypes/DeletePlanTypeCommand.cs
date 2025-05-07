using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BCP.PlanTypes;

public class DeletePlanTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}