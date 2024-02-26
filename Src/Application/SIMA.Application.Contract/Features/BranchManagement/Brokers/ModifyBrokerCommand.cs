using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.BranchManagement.Brokers;

public class ModifyBrokerCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public long BrokerTypeId { get; set; }
    public DateTime? ExpireDate { get; set; }
    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }
}
