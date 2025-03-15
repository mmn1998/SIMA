using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.OperationalStatuses;

public class DeleteOperationalStatusCommand: ICommand<Result<long>>
{
    public long Id { get; set; }
}