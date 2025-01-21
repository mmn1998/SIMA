using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.WageDeductionMethods;

public class DeleteWageDeductionMethodCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}