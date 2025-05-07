using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.OperationalStatuses;

public class CreateOperationalStatusCommand: ICommand<Result<long>>
{
    public string Name { get; set; }
    public string Code { get; set; }
}