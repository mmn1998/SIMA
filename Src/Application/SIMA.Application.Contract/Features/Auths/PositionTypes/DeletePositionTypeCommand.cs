using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.PositionTypes;

public class DeletePositionTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}