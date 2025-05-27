using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItems;

public class DeleteConfigurationItemCommand :  ICommand<Result<long>>
{
    public long Id { get; set; }
}
