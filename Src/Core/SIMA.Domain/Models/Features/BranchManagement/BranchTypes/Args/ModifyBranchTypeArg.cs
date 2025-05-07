namespace SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Args
{
    public class ModifyBranchTypeArg
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? ActiveStatusId { get; set; }
        public byte[]? ModifyAt { get; set; }
        public long? ModifyBy { get; set; }
    }
}
