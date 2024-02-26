using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.AddressTypes;

public class ModifyAddressTypeCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long ActiveStatusId { get; set; }

    public string? Code { get; set; }
}
