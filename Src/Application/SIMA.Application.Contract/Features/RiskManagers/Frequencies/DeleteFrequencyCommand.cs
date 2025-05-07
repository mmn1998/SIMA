using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.Frequencies;

public class DeleteFrequencyCommand: ICommand<Result<long>>
{
    public long Id { get; set; }
}
