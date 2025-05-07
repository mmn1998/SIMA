using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.WageRates;

public class DeleteWageRateCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}