using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.ConfigurationAttributes;

public class DeleteConfigurationAttributeByIdCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
