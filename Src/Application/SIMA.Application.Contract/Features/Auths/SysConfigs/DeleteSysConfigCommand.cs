using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.SysConfigs;

public class DeleteSysConfigCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}
