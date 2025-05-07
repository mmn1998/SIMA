using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.TrustyDrafts.BrokerSecondLevelAddressBooks;

public class DeleteBrokerSecondLevelAddressBookCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}