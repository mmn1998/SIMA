using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.PositionLevels;

public class CreatePositionLevelCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
}