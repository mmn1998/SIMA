using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemTypes;

public class DeleteConfigurationItemTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}