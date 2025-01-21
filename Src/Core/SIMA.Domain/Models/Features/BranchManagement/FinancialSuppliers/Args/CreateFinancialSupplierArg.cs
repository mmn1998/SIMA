namespace SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Args
{
    public class CreateFinancialSupplierArg
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
