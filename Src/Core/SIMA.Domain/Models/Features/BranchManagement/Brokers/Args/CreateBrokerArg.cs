namespace SIMA.Domain.Models.Features.BranchManagement.Brokers.Args;

public class CreateBrokerArg
{
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long? BrokerTypeId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime? ExpireDate { get; set; }

    public int? ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long CreatedBy { get; set; }
}
