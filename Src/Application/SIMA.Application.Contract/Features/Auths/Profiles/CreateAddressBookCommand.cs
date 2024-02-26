using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.Profiles;
public class CreateAddressBookCommand : ICommand<Result<long>>
{
    public long ProfileId { get; set; }

    public long? AddressTypeId { get; set; }
    public long? LocationId { get; set; }

    public string? Address { get; set; }

    public string? PostalCode { get; set; }

    public long ActiveStatusId { get; set; }
}
