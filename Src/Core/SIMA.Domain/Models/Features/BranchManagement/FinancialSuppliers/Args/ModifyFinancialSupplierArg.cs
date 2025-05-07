namespace SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Args
{
    public class ModifyFinancialSupplierArg
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
