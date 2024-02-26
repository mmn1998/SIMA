namespace SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Args
{
    public class ModifyBrokerTypeArg
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
