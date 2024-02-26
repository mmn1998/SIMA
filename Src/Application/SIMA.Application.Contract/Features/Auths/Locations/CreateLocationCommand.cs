using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Locations;

public class CreateLocationCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long LocationTypeId { get; set; }

    public long ParentId { get; set; }

}
