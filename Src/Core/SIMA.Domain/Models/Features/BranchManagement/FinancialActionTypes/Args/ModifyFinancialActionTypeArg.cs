namespace SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Args
{
    public class ModifyFinancialActionTypeArg
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
