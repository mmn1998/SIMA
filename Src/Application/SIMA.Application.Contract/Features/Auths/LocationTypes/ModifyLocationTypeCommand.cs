using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.LocationTypes;

public class ModifyLocationTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }

    public long ParentId { get; set; }

}
