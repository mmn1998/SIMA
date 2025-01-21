using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemStatuses;

public class DeleteConfigurationItemStatusCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}