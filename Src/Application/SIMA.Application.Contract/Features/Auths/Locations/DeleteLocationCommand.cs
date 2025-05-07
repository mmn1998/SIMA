using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Locations;
public class DeleteLocationCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}