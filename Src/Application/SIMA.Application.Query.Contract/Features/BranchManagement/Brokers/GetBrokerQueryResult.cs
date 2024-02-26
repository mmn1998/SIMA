namespace SIMA.Application.Query.Contract.Features.BranchManagement.Brokers;

public class GetBrokerQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long? BrokerTypeId { get; set; }
    public long? ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? ExpireDatePersian { get; set; }
}
