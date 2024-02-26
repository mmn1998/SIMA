using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;
using System.Windows.Input;

namespace SIMA.Application.Contract.Features.Auths.Profiles;

public class ModifyAddressBookCommand : ICommand<Result<long>>
{
    public long Id { get; set; }

    public long ProfileId { get; set; }

    public long? AddressTypeId { get; set; }

    public string? Address { get; set; }

    public string? PostalCode { get; set; }

    public long ActiveStatusId { get; set; }
}
