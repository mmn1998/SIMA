using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.SysConfigs;

public class CreateSystemConfigurationCommand : ICommand<Result<long>>
{
    public long? ConfigurationId { get; set; }
    public string? KeyValue { get; private set; }
}
