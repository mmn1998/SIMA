using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.BrokerSecondLevelAddressBooks;

public class CreateBrokerSecondLevelAddressBookCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}