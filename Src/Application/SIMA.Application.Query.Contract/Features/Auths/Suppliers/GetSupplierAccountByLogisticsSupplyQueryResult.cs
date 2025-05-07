namespace SIMA.Application.Query.Contract.Features.Auths.Suppliers
{
    public class GetSupplierAccountByLogisticsSupplyQueryResult
    {
        public long Id { get; set; }
        public long SupplierId { get; set; }
        public string Name { get; set; }
        public long CandidatedSupplier { get; set; }
        public long LogisticsSupplyId { get; set; }
    }
}
