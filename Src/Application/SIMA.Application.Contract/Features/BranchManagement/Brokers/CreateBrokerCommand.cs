using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BranchManagement.Brokers;

public class CreateBrokerCommand : ICommand<Result<long>>
{
    public long BrokerTypeId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ExpireDate { get; set; }
    public List<CreateBrokerPhoneBookCommand>? BrokerPhoneBookList { get; set; }
    public List<CreateBrokerAddressBookCommand>? BrokerAddressBookList { get; set; }
    public List<string>? BrokerAccountBookList { get; set; }
}