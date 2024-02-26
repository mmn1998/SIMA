using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.AddressTypes;

public class CreateAddressTypeCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
}
