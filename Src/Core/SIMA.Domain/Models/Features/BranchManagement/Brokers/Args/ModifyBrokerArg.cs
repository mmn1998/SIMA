namespace SIMA.Domain.Models.Features.BranchManagement.Brokers.Args;

public class ModifyBrokerArg
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public long? BrokerTypeId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime? ExpireDate { get; set; }

    public int? ActiveStatusId { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
