using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.LocationTypes;
public class DeleteLocationTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}